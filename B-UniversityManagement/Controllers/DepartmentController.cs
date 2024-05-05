using B_UniversityManagement.DTOs;
using B_UniversityManagement.IRepository;
using B_UniversityManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace B_UniversityManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepo departmentRepo;

        public DepartmentController(IDepartmentRepo departmentRepo)
        {
            this.departmentRepo = departmentRepo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var departments = departmentRepo.GetAll();
            var departmentDTOs = TransferDepartment.TransferListToDto(departments);
            return Ok(departmentDTOs);
        }


        [HttpPost]
        public IActionResult Create(DepartmentDTO departmentDTO)
        {
            if (ModelState.IsValid)
            {
                var department = TransferDepartment.TransferDtoToDepartment(departmentDTO);
                departmentRepo.Create(department);
                return Ok(departmentDTO);
            }
            return BadRequest();
        }
    }
}
