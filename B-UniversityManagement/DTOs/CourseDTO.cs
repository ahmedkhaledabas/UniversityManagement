using B_UniversityManagement.Enums;
using B_UniversityManagement.Models;

namespace B_UniversityManagement.DTOs
{
    public class CourseDTO
    {
        public string Id { get; set; } = null!;
        public LevelYear LevelYear { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Img { get; set; }
        public string ProfessorId { get; set; } = null!;
        public string DepartmentId { get; set; } = null!;
        public decimal Price { get; set; }
    }
}
