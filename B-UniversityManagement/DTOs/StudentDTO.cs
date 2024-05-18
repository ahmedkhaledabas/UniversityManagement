using B_UniversityManagement.Enums;
using B_UniversityManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace B_UniversityManagement.DTOs
{
    public class StudentDTO
    {
        public string Id { get; set; }
        public string FName { get; set; } = null!;
        public string? LName { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public Gender Gender { get; set; }
        public string? Img { get; set; }
        public string Code { get; set; } = null!;
        public LevelYear levelYear { get; set; } = LevelYear.First;
        public int DepartmentId { get; set; }
    }
}
