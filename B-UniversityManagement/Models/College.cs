using System.ComponentModel.DataAnnotations.Schema;

namespace B_UniversityManagement.Models
{
    public class College 
    {
        public string Id { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public string? Img { get; set; } = "http://localhost:5278//Uploads/Common/default.png";

        public List<Department> Departments { get; set; } = new List<Department> { };
        public List<User> Users { get; set; } = new List<User> { };
        public Library? Library { get; set; }

        //
        // public IFormFile? ImageFile {  get; set; }
        // img
        
    } 
}
