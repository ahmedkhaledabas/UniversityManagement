using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using B_UniversityManagement.Data;
using B_UniversityManagement.Models;
using B_UniversityManagement.IRepository;
using B_UniversityManagement.Services;
using B_UniversityManagement.DTOs;

namespace B_UniversityManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollegeController : ControllerBase
    {
        private readonly ICollegeRepo collegeRepo;
        private readonly IWebHostEnvironment webHostEnvironment;

        public CollegeController(ICollegeRepo collegeRepo , IWebHostEnvironment webHostEnvironment)
        {
            this.collegeRepo = collegeRepo;
            this.webHostEnvironment = webHostEnvironment;
        }

        // GET: api/College
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CollegeDTO>>> GetColleges()
        {
            var colleges = collegeRepo.GetAll();
          if (colleges != null)
          {
                var collegesDto = TransferCollege.TransferListToDto(colleges);
                return Ok( collegesDto);
          }
           return NotFound();
        }

        // GET: api/College/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CollegeDTO>> GetCollege(int id)
        {
            var college = collegeRepo.GetById(id);
            if(college != null)
            {
                var collegeDto = TransferCollege.TransferCollegeToDto(college);
                return Ok(collegeDto);
            }
            return NotFound();
        }

        // PUT: api/College/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCollege(int id, College college)
        {
            var collegeFind = collegeRepo.GetById(id);
            if (collegeFind != null)
            {
                collegeRepo.Update(college);
                return Ok(college);
            }
            return NoContent();
        }

        // POST: api/College
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<College>> PostCollege(CollegeDTO collegeDto)
        {
            College college = TransferCollege.TransferDtoToCollege(collegeDto);
            collegeRepo.Create(college);
            return Ok(collegeDto);
        }

        // DELETE: api/College/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollege(int id)
        {
            var collegeFind = collegeRepo.GetById(id);
            if (collegeFind != null)
            {
                collegeRepo.Delete(collegeFind);
                return Ok(collegeFind);
            }
            return NoContent();
        }
        
        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImage()
        {
            bool result = false;
            try
            {
                var uploadFiles = Request.Form.Files;
                foreach (IFormFile source in uploadFiles)
                {
                    string originalFileName = source.FileName;
                    string filePath = GetFilePath(originalFileName);
                    var fileExtension = Path.GetExtension(originalFileName);
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    var uploadedFilePath = Path.Combine(filePath, $"image{fileExtension}");

                    if (System.IO.File.Exists(uploadedFilePath))
                    {
                        System.IO.File.Delete(uploadedFilePath);
                    }
                    using(FileStream stream=System.IO.File.Create(uploadedFilePath))
                    {
                        await source.CopyToAsync(stream);
                        result = true;
                    }
                }
            }
            catch(Exception ex)
            {

            }
            return Ok(result);

        }
        
        [NonAction]
        private string GetFilePath(string collegeCode)
        {
            // Use the `IWebHostEnvironment` interface instead of `this.webHostEnvironment`
            var webRootPath = webHostEnvironment.WebRootPath;
            return Path.Combine(webRootPath, "Uploads", "Colleges", collegeCode);
        }
    }
}
