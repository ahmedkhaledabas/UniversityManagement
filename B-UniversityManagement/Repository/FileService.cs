using B_UniversityManagement.IRepository;

namespace B_UniversityManagement.Repository
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment environment;

        public FileService(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }
        public bool DeleteImage(string imageFileName)
        {
            try
            {
                var wwwPath = this.environment.WebRootPath;
                var path = Path.Combine(wwwPath, "Uploads\\Colleges\\", imageFileName);
                if(System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public Tuple<int, string> SaveImage(IFormFile imageFile)
        {
            try
            {
                var contentPath = this.environment.ContentRootPath;
                var path = Path.Combine(contentPath, "Uploads\\Colleges");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //check allowed extension
                var extension = Path.GetExtension(imageFile.FileName);
                var allowedExtentions = new string[] { ".jpg", ".png", ".jpeg" };
                if (!allowedExtentions.Contains(extension))
                {
                    string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtentions));
                    return new Tuple<int, string>(0, msg);
                }
                string uniqueString = Guid.NewGuid().ToString();
                //try to create uniqe fileName
                var newFileName = uniqueString + extension;
                var fileWithPath = Path.Combine(path, newFileName);
                var stream = new FileStream(fileWithPath, FileMode.Create);
                imageFile.CopyTo(stream);
                stream.Close();
                return new Tuple<int, string>(1, newFileName);
            }
            catch
            {
                return new Tuple<int, string>(0, "Error has occured");
            }
        }
    }
}
