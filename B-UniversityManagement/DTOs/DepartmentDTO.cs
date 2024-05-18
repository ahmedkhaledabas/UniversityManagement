using B_UniversityManagement.Models;

namespace B_UniversityManagement.DTOs
{
    public class DepartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string CollegeId { get; set; }
    }
}
