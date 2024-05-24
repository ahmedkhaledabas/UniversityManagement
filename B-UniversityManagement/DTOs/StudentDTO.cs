using B_UniversityManagement.Enums;
using B_UniversityManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace B_UniversityManagement.DTOs
{
    public class StudentDTO
    {

        public string UserName { get; set; } = null!;
        public string Id { get; set; } = null!;
        public string FName { get; set; } = null!;
        public string? LName { get; set; }
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string? Address { get; set; }
        public Gender Gender { get; set; }
        public string? Img { get; set; }
        public LevelYear levelYear { get; set; } = LevelYear.First;
        public string DepartmentId { get; set; } = null!;
        public string CollegeId { get; set; } = null!;
    }
}
