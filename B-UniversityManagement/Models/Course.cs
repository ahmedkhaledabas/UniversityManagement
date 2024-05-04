using B_UniversityManagement.Enums;
using Microsoft.Build.Framework;

namespace B_UniversityManagement.Models
{
    public class Course : BaseProperties
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Img { get; set; }
        public List<Student> Students { get; set; } = null!;
        public Guid ProfessorId { get; set; }
        public Professor Professor { get; set; } = null!;
        public int DepartmentId { get; set; } 
        public Department? Department {  get; set; }
        // img , pdfFile ,
    }
}
