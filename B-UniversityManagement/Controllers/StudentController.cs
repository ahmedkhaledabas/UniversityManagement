using B_UniversityManagement.DTOs;
using B_UniversityManagement.IRepository;
using B_UniversityManagement.Models;
using B_UniversityManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace B_UniversityManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ICourseRepo courseRepo;
        private readonly IStudentCourses studentCoursesRepo;
        private readonly IEmailSenderRepo emailSenderRepo;
        private readonly ICollegeRepo collegeRepo;

        public StudentController(UserManager<User> userManager ,SignInManager<User> signInManager, IWebHostEnvironment webHostEnvironment,
            ICourseRepo courseRepo, IStudentCourses studentCoursesRepo , IEmailSenderRepo emailSenderRepo, ICollegeRepo collegeRepo)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.webHostEnvironment = webHostEnvironment;
            this.courseRepo = courseRepo;
            this.studentCoursesRepo = studentCoursesRepo;
            this.emailSenderRepo = emailSenderRepo;
            this.collegeRepo = collegeRepo;
        }

        [HttpGet("GetStudents")]
        public IActionResult GetStudents()
        {
            var students = userManager.GetUsersInRoleAsync("Student").Result.ToList();
            var studentDTOs = TransferStudent.ListOfStudentToDTOs(students);
            return Ok(studentDTOs);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Registeration([FromForm]StudentDTO studentDTO)
        {
            if (ModelState.IsValid)
            {
                UploadImage(studentDTO.UserName);
                studentDTO.Img = GetImageCollege(studentDTO.UserName);
                var student = TransferStudent.TransferDTOToStudent(studentDTO);
                //for each of courses , add course where course.level year == student.levelyear
               
                var created = await userManager.CreateAsync(student, studentDTO.PasswordHash);
                if (created.Succeeded)
                {
                    var collegeName = collegeRepo.GetById(student.CollegeId).Name;
                    emailSenderRepo.SendEmail(student , studentDTO.PasswordHash , collegeName);
                    var courses = courseRepo.GetAll();
                    foreach (var course in courses)
                    {
                        if (course.LevelYear == student.levelYear && course.DepartmentId == student.DepartmentId)
                        {
                            var studentCourse = new StudentCourse
                            {
                                CourseId = course.Id,
                                StudentId = student.Id,
                                Degree = 0,

                            };
                            studentCoursesRepo.Create(studentCourse);
                        }
                        else continue;
                    }
                    var addRole = await userManager.AddToRoleAsync(student, "Student");
                    return Ok(studentDTO);
                }
                foreach (var error in created.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(created.Errors);
            }
            return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> PutStudent([FromForm] StudentDTO studentDTO)
        {
            if (ModelState.IsValid)
            {
                var student = await userManager.FindByNameAsync(studentDTO.UserName);
                if(Request.Form.Files.Count > 0)
                {
                    Random random = new Random();
                    UploadImage(studentDTO.UserName + random);
                    studentDTO.Img = GetImageCollege(studentDTO.UserName + random);
                    
                    
                }
                if (student != null)
                {
                    student.FName = studentDTO.FName;
                    student.LName = studentDTO.LName;
                    student.Email = studentDTO.Email;
                    student.PasswordHash = studentDTO.PasswordHash;
                    student.Gender = studentDTO.Gender;
                    student.Phone = studentDTO.Phone;
                    student.BirthDate = studentDTO.BirthDate;
                    student.Img = studentDTO.Img == "undefined" ? student.Img : studentDTO.Img;
                    student.Address = studentDTO.Address;
                    student.CollegeId = studentDTO.CollegeId;
                    student.DepartmentId = studentDTO.DepartmentId;
                    student.levelYear = studentDTO.levelYear;
                }
                else return NotFound();
                var result = await userManager.UpdateAsync(student);
                if (result.Succeeded)
                {
                    return Ok(studentDTO);
                }
                else
                {
foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                    return BadRequest(ModelState);
                }

                
            }
            else return BadRequest(ModelState);
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
        private string GetFilePath(string collegeName)
        {
            return this.webHostEnvironment.WebRootPath + "\\Uploads\\Students\\" + collegeName;
        }

        [NonAction]
        private string GetImageCollege(string collegeName)
        {
            string imageUrl = string.Empty;
            string hostUrl = "http://localhost:5278/";
            string filePath = GetFilePath(collegeName);
            string imagePath = filePath + "\\image.png";
            if (Directory.Exists(imagePath))
            {
                imageUrl = hostUrl + "/Uploads/Students/" + collegeName + "/image.png";
            }
            else
            {
                imageUrl = hostUrl + "/Uploads/Students/" + collegeName + "/image.png";
                //imageUrl = hostUrl + "/Uploads/Common/default.png";
            }
            return imageUrl;
        }

    }
}