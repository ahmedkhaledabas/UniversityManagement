using B_UniversityManagement.DTOs;
using B_UniversityManagement.IRepository;
using B_UniversityManagement.Models;
using B_UniversityManagement.Repository;
using B_UniversityManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace B_UniversityManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepo courseRepo;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IStudentCourses studentCoursesRepo;
        private readonly UserManager<User> userManager;

        public CourseController(ICourseRepo courseRepo, IWebHostEnvironment webHostEnvironment , IStudentCourses studentCoursesRepo, UserManager<User> userManager)
        {
            this.courseRepo = courseRepo;
            this.webHostEnvironment = webHostEnvironment;
            this.studentCoursesRepo = studentCoursesRepo;
            this.userManager = userManager;
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

        [HttpGet("userName")]
        public async Task<ActionResult<List<CourseDTO>>> GetMyCourses(string userName)
        {
            var user =  await userManager.FindByNameAsync(userName);
            var courseDTOs = new List<CourseDTO>();
            var studentCourses = studentCoursesRepo.GetAll();
           
            foreach (var item in studentCourses)
            {
                if (item.StudentId == user.Id)
                {
                    var course = courseRepo.GetById(item.CourseId);
                    var courseDTO = TransferCourse.CourseToDTO(course);
                    courseDTOs.Add(courseDTO);
                }
                else continue;
            }
            return courseDTOs;
        }

        [HttpGet]
        [Route("getForUser")]
        public async Task<ActionResult<List<CourseDTO>>> GetCoursesForUser(string userName)
        {
            List<Course> courses = courseRepo.GetAll();
            var user = await userManager.FindByNameAsync(userName);
            var hisCourses = studentCoursesRepo.GetAllForStudent(user.Id);
            foreach (var item in hisCourses)
            {
                var course = courseRepo.GetById(item.CourseId);
                courses.Remove(course);
            }
            var dtos = TransferCourse.ListCourseToDTOs(courses);
            return Ok(dtos);


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
                        var studentCourse = new StudentCourse
                        {
                            CourseId = course.Id,
                            StudentId = course.UserId,
                            Degree = 0,

                        };
                        studentCoursesRepo.Create(studentCourse);
                    
                return Ok(courseDTO);

            }
            return BadRequest();

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCourse(string id, [FromForm] CourseDTO courseDTO)
        {
            var course = courseRepo.GetById(id);
            if (Request.Form.Files.Count > 0)
            {
                if (course != null)
                {
                    Random random = new Random();
                    UploadImage(courseDTO.Id + random);
                    courseDTO.Img = GetImageCollege(courseDTO.Id + random);
                    var courss = TransferCourse.DTOToCourse(courseDTO);
                    courseRepo.Update(courss);
                    return Ok(courseDTO);
                }
                else
                {
                    return NoContent();
                }
            }
            else
            {
                //var collegeFind = collegeRepo.GetById(id);
                if (course != null)
                {
                    var courss = TransferCourse.DTOToCourse(courseDTO);
                    courss.Img = course.Img;
                    courseRepo.Update(courss);
                    return Ok(courseDTO);
                }
                else return NoContent();
            }

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(string id)
        {
            var courseFind = courseRepo.GetById(id);
            if (courseFind != null)
            {
                courseRepo.Delete(courseFind);
                return Ok(courseFind);
            }
            return NoContent();
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
