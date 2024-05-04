using B_UniversityManagement.Enums;

namespace B_UniversityManagement.Models
{
    public class Professor : User
    { 
        public string? Specialist { get; set; }
        public string? Img { get; set; }
        public List<College> Colleges { get; set; } = null!;
        public List<Course>? Courses { get; set; }
    }
}
