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
        private readonly UserManager<Student> userManager;
        private readonly SignInManager<Student> signInManager;

        public StudentController(UserManager<Student> userManager ,SignInManager<Student> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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
        public async Task<IActionResult> Registeration(StudentDTO studentDTO)
        {
            if (ModelState.IsValid)
            {
                var student = TransferStudent.TransferDTOToStudent(studentDTO);
                
                var created = await userManager.CreateAsync(student, studentDTO.PasswordHash);
                if (created.Succeeded)
                {
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

        
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(userLoginDTO.UserName);
                if (user != null)
                {
                    var checkPass = await userManager.CheckPasswordAsync(user, userLoginDTO.Password);
                    if (checkPass)
                    {
                        await signInManager.SignInAsync(user, userLoginDTO.RememberMe);
                        return Ok(userLoginDTO);
                    }
                    ModelState.AddModelError(string.Empty, "Invalid UserName Or Password");
                }
                return BadRequest("Invalid UserName Or Password");
            }
            ModelState.AddModelError(string.Empty, "Invalid UserName Or Password");
            return BadRequest("Invalid UserName Or Password");
        }


        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }

       

    }
}