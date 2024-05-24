using B_UniversityManagement.DTOs;
using B_UniversityManagement.IRepository;
using B_UniversityManagement.Models;
using B_UniversityManagement.Repository;
using B_UniversityManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace B_UniversityManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepo courseRepo;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CourseController(ICourseRepo courseRepo , IWebHostEnvironment webHostEnvironment)
        {
            this.courseRepo = courseRepo;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetCourses()
        {
            var courses = courseRepo.GetAll();
            var couresDtos = TransferCourse.ListCourseToDTOs(courses);
            if (couresDtos != null && couresDtos.Count > 0)
            {
                return Ok(couresDtos);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        public async Task<ActionResult<Course>> PostCourse([FromForm] CourseDTO courseDTO)
        {
            if (ModelState.IsValid)
            {
                UploadImage(courseDTO.Name);
                courseDTO.Img = GetImageCollege(courseDTO.Name);
                Course course = TransferCourse.DTOToCourse(courseDTO);
                courseRepo.Create(course);
                return Ok(courseDTO);

            }
            return BadRequest();

        }


        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage(string name)
        {
            bool result = false;
            try
            {
                var uploadFiles = Request.Form.Files;
                foreach (IFormFile source in uploadFiles)
                {
                    //string originalFileName = source.FileName;
                    string filePath = GetFilePath(name);
                    //var fileExtension = Path.GetExtension(originalFileName);
                    if (!System.IO.Directory.Exists(filePath))
                    {
                        System.IO.Directory.CreateDirectory(filePath);
                    }
                    string imagePath = filePath + "\\image.png";

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                    using (FileStream stream = System.IO.File.Create(imagePath))
                    {
                        await source.CopyToAsync(stream);
                        result = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Ok(result);

        }

        [NonAction]
        private string GetFilePath(string name)
        {
            return this.webHostEnvironment.WebRootPath + "\\Uploads\\Courses\\" + name;
        }

        [NonAction]
        private string GetImageCollege(string name)
        {
            string imageUrl = string.Empty;
            string hostUrl = "http://localhost:5278/";
            string filePath = GetFilePath(name);
            string imagePath = filePath + "\\image.png";
            if (Directory.Exists(imagePath))
            {
                imageUrl = hostUrl + "/Uploads/Courses/" + name + "/image.png";
            }
            else
            {
                imageUrl = hostUrl + "/Uploads/Courses/" + name + "/image.png";
                //imageUrl = hostUrl + "/Uploads/Common/default.png";
            }
            return imageUrl;
        }
    }
}
