using B_UniversityManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace B_UniversityManagement.DTOs
{
    public class CollegeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? Img { get; set; }
    }
}

