using B_UniversityManagement.Enums;

namespace B_UniversityManagement.DTOs
{
    public class EmployeeDTO
    {
        public decimal? EmpSalary { get; set; }
        public string CollegeId { get; set; } = null!;
        public string Id { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string FName { get; set; } = null!;
        public string? LName { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public Gender Gender { get; set; }
        public string? Img { get; set; }
    }
}
