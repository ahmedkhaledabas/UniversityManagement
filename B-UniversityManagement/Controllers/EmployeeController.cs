using B_UniversityManagement.DTOs;
using B_UniversityManagement.Models;
using B_UniversityManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace B_UniversityManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public EmployeeController(UserManager<User> userManager, SignInManager<User> signInManager, IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult GetEmps()
        {
            var emps = userManager.GetUsersInRoleAsync("Employee").Result.ToList();
            var empDTOs = TransferEmployee.ListEmpsToDTOs(emps);
            return Ok(empDTOs);
        }

        [HttpPost]
        public async Task<IActionResult> Registeration([FromForm] EmployeeDTO employeeDTO)
        {
            if (ModelState.IsValid)
            {
                UploadImage(employeeDTO.UserName);
                employeeDTO.Img = GetImageCollege(employeeDTO.UserName);
                var emp = TransferEmployee.DTOToEmployee(employeeDTO);

                var created = await userManager.CreateAsync(emp, employeeDTO.Password);
                if (created.Succeeded)
                {
                    var addRole = await userManager.AddToRoleAsync(emp, "Employee");
                    return Ok(employeeDTO);
                }
                foreach (var error in created.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return BadRequest(created.Errors);
            }
            return BadRequest();
        }


        [HttpPost("Login")]
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


        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok();
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
            return this.webHostEnvironment.WebRootPath + "\\Uploads\\Employees\\" + collegeName;
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
                imageUrl = hostUrl + "/Uploads/Employees/" + collegeName + "/image.png";
            }
            else
            {
                imageUrl = hostUrl + "/Uploads/Employees/" + collegeName + "/image.png";
                //imageUrl = hostUrl + "/Uploads/Common/default.png";
            }
            return imageUrl;
        }
    }
}
