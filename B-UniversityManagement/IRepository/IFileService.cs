namespace B_UniversityManagement.IRepository
{
    public interface IFileService
    {
        public Tuple<int, string> SaveImage(IFormFile imageFile);
        public bool DeleteImage(string imageFileName);
    }
}
