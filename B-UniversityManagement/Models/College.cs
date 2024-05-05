using System.ComponentModel.DataAnnotations.Schema;

namespace B_UniversityManagement.Models
{
    public class College : BaseProperties
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Img { get; set; }
        public List<Department> Departments { get; set; } = new List<Department> { };
        public List<Employee> Employees { get; set; } = new List<Employee> { };
        public Library? Library {  get; set; }
        public List<Professor> Professors { get; set; } = new List<Professor> { };

        [NotMapped]
        public IFormFile? ImageFile {  get; set; }
        // img , 
    }
}
