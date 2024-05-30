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
using Microsoft.AspNetCore.Mvc.ApiExplorer;

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
            var collegesDto = TransferCollege.TransferListToDto(colleges);
          if (collegesDto != null && collegesDto.Count > 0)
          {
                return Ok( collegesDto);
            }
            else
            {
                return NotFound();
            }
           
        }

        // GET: api/College/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CollegeDTO>> GetCollege(string id)
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
        public async Task<IActionResult> PutCollege(string id , [FromForm]CollegeDTO collegeDto)
        {
            var college = collegeRepo.GetById(id);
            if(Request.Form.Files.Count > 0)
            {
                if (college != null)
                {
                    Random random = new Random();
                    UploadImage(collegeDto.Id + random);
                    collegeDto.Img = GetImageCollege(collegeDto.Id + random);
                    var coll = TransferCollege.TransferDtoToCollege(collegeDto);
                    collegeRepo.Update(coll);
                    return Ok(collegeDto);
                }
                else
                {
                    return NoContent();
                }
            }
            else
            {
                //var collegeFind = collegeRepo.GetById(id);
                  if (college != null)
                  {
                  var colleget = TransferCollege.TransferDtoToCollege(collegeDto);
                  collegeRepo.Update(colleget);
                    return Ok(collegeDto);
                  }
                  else return NoContent();
            }
           
        }

        // POST: api/College
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<College>> PostCollege([FromForm]CollegeDTO collegeDto)
        {
            if (ModelState.IsValid)
            {
                UploadImage(collegeDto.Id);
                collegeDto.Img = GetImageCollege(collegeDto.Id);
                College college = TransferCollege.TransferDtoToCollege(collegeDto);
                    collegeRepo.Create(college);
                    return Ok(collegeDto);
                
            }
            return BadRequest();
            
        }

        // DELETE: api/College/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollege(string id)
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
        public async Task<IActionResult> UploadImage(string name)
        {
            bool result = false;
            try
            {

                var uploadFiles = Request.Form.Files;
                foreach (IFormFile source in uploadFiles)
                {
                    string originalFileName = source.FileName;
                    string filePath = GetFilePath(name);
                    var fileExtension = Path.GetExtension(originalFileName);
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
            return this.webHostEnvironment.WebRootPath + "\\Uploads\\Colleges\\" + collegeName;
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
                imageUrl = hostUrl + "/Uploads/Colleges/" + collegeName + "/image.png";
            }
            else
            {
                imageUrl = hostUrl + "/Uploads/Colleges/" + collegeName + "/image.png";
                //imageUrl = hostUrl + "/Uploads/Common/default.png";
            }
            return imageUrl;
        }
    }
}
