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
    public class ProfessorController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IEmailSenderRepo emailSenderRepo;
        private readonly ICollegeRepo collegeRepo;

        public ProfessorController(UserManager<User> userManager, IWebHostEnvironment webHostEnvironment, IEmailSenderRepo emailSenderRepo, ICollegeRepo collegeRepo)
        {
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
            this.emailSenderRepo = emailSenderRepo;
            this.collegeRepo = collegeRepo;
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
                    var collegeName = collegeRepo.GetById(professor.CollegeId).Name;
                    emailSenderRepo.SendEmail(professor, professorDTO.Password, collegeName);
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

        [HttpPut]
        public async Task<IActionResult> PutStudent([FromForm] ProfessorDTO professorDTO)
        {
            if (ModelState.IsValid)
            {
                var professor = await userManager.FindByNameAsync(professorDTO.UserName);
                if (Request.Form.Files.Count > 0)
                {
                    Random random = new Random();
                    UploadImage(professorDTO.UserName + random);
                    professorDTO.Img = GetImageProfessor(professorDTO.UserName + random);


                }
                if (professor != null)
                {
                    professor.FName = professorDTO.FName;
                    professor.LName = professorDTO.LName;
                    professor.Email = professorDTO.Email;
                    professor.PasswordHash = professorDTO.Password;
                    professor.Gender = professorDTO.Gender;
                    professor.Phone = professorDTO.Phone;
                    professor.BirthDate = professorDTO.BirthDate;
                    professor.Img = professorDTO.Img == "undefined" ? professor.Img : professorDTO.Img;
                    professor.Address = professorDTO.Address;
                    professor.CollegeId = professorDTO.CollegeId;
                    professor.DepartmentId = professorDTO.DepartmentId;
                    professor.Rank = professorDTO.Rank;
                    professor.Specialist = professorDTO.Specialist;
                }
                else return NotFound();
                var result = await userManager.UpdateAsync(professor);
                if (result.Succeeded)
                {
                    return Ok(professorDTO);
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
