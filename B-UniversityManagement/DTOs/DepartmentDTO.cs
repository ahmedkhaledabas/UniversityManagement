using B_UniversityManagement.Models;

namespace B_UniversityManagement.DTOs
{
    public class DepartmentDTO
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string CollegeId { get; set; } = null!;
    }
}
