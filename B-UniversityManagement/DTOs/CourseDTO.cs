using B_UniversityManagement.Enums;
using B_UniversityManagement.Models;

namespace B_UniversityManagement.DTOs
{
    public class CourseDTO
    {
        public string Id { get; set; }
        public LevelYear LevelYear { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Img { get; set; }
        public string ProfessorId { get; set; }
        public int DepartmentId { get; set; }
    }
}
