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
    public class ProfessorController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ProfessorController(UserManager<User> userManager , SignInManager<User> signInManager , IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("GetProfessors")]
        public IActionResult GetProfessors()
        {
            var professors = userManager.GetUsersInRoleAsync("Professor").Result.ToList();
            var professorsDTOs = TransferProfessor.ListProfessorToDTOs(professors);
            return Ok(professorsDTOs);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Registeration([FromForm] ProfessorDTO professorDTO)
        {
            if (ModelState.IsValid)
            {
                UploadImage(professorDTO.UserName);
                professorDTO.Img = GetImageProfessor(professorDTO.UserName);
                var professor = TransferProfessor.DTOToProfessor(professorDTO);

                var created = await userManager.CreateAsync(professor, professorDTO.Password);
                if (created.Succeeded)
                {
                    var addRole = await userManager.AddToRoleAsync(professor, "Professor");
                    return Ok(professorDTO);
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
            return this.webHostEnvironment.WebRootPath + "\\Uploads\\Professors\\" + collegeName;
        }

        [NonAction]
        private string GetImageProfessor(string collegeName)
        {
            string imageUrl = string.Empty;
            string hostUrl = "http://localhost:5278/";
            string filePath = GetFilePath(collegeName);
            string imagePath = filePath + "\\image.png";
            if (Directory.Exists(imagePath))
            {
                imageUrl = hostUrl + "/Uploads/Professors/" + collegeName + "/image.png";
            }
            else
            {
                imageUrl = hostUrl + "/Uploads/Professors/" + collegeName + "/image.png";
                //imageUrl = hostUrl + "/Uploads/Common/default.png";
            }
            return imageUrl;
        }
    }
}
