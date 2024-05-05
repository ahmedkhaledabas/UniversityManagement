using B_UniversityManagement.Models;

namespace B_UniversityManagement.DTOs
{
    public class DepartmentDTO
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int CollegeId { get; set; }
    }
}
