using B_UniversityManagement.DTOs;
using B_UniversityManagement.IRepository;
using B_UniversityManagement.Models;
using B_UniversityManagement.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace B_UniversityManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IEmailSenderRepo emailSenderRepo;
        private readonly ICollegeRepo collegeRepo;

        public EmployeeController(UserManager<User> userManager, IWebHostEnvironment webHostEnvironment, IEmailSenderRepo emailSenderRepo, ICollegeRepo collegeRepo)
        {
            this.userManager = userManager;
            this.webHostEnvironment = webHostEnvironment;
            this.emailSenderRepo = emailSenderRepo;
            this.collegeRepo = collegeRepo;
        }

        [HttpGet]
        public IActionResult GetEmps()
        {
            var emps = userManager.GetUsersInRoleAsync("Employee").Result.ToList();
            var empDTOs = TransferEmployee.ListEmpsToDTOs(emps);
            return Ok(empDTOs);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Registeration([FromForm] EmployeeDTO employeeDTO)
        {
            if (ModelState.IsValid)
            {
                UploadImage(employeeDTO.UserName);
                employeeDTO.Img = GetImageEmployee(employeeDTO.UserName);
                var emp = TransferEmployee.DTOToEmployee(employeeDTO);

                var created = await userManager.CreateAsync(emp, employeeDTO.Password);
                if (created.Succeeded)
                {
                    var collegeName = collegeRepo.GetById(emp.CollegeId).Name;
                    //emailSenderRepo.SendEmail(emp, employeeDTO.Password, collegeName);
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

        [HttpPut]
        public async Task<IActionResult> PutEmployee([FromForm] EmployeeDTO employeeDTO)
        {
            if (ModelState.IsValid)
            {
                var employee = await userManager.FindByNameAsync(employeeDTO.UserName);
                if (Request.Form.Files.Count > 0)
                {
                    Random random = new Random();
                    UploadImage(employeeDTO.UserName + random);
                    employeeDTO.Img = GetImageEmployee(employeeDTO.UserName + random);


                }
                if (employee != null)
                {
                    employee.FName = employeeDTO.FName;
                    employee.LName = employeeDTO.LName;
                    employee.Email = employeeDTO.Email;
                    employee.PasswordHash = employeeDTO.Password;
                    employee.Gender = employeeDTO.Gender;
                    employee.Phone = employeeDTO.Phone;
                    employee.BirthDate = employeeDTO.BirthDate;
                    employee.Img = employeeDTO.Img == "undefined" ? employee.Img : employeeDTO.Img;
                    employee.Address = employeeDTO.Address;
                    employee.CollegeId = employeeDTO.CollegeId;
                    employee.DepartmentId = employeeDTO.DepartmentId;
                    employee.EmpSalary = employeeDTO.EmpSalary;
                }
                else return NotFound();
                var result = await userManager.UpdateAsync(employee);
                if (result.Succeeded)
                {
                    return Ok(employeeDTO);
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
            return this.webHostEnvironment.WebRootPath + "\\Uploads\\Employees\\" + collegeName;
        }

        [NonAction]
        private string GetImageEmployee(string collegeName)
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
