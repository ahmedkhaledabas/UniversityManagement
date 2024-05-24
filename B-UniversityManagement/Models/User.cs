using B_UniversityManagement.Enums;
using Microsoft.AspNetCore.Identity;

namespace B_UniversityManagement.Models
{
    public class User  : IdentityUser
    {
        //public string Id { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string FName { get; set; } = null!;
        public string? LName { get; set; }
        public string Phone { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string? Address { get; set; }
        public Gender Gender { get; set; }
        public string? Img { get; set; }

        //employee
        public decimal? EmpSalary { get; set; }
        public string? CollegeId { get; set; }
        public College? College { get; set; } 

        //proff
        public string? Specialist { get; set; }
        public List<Course> Courses { get; set; } = new List<Course>();

        public AcademicRank Rank { get; set; } = AcademicRank.Professor;

        //student
        public LevelYear levelYear { get; set; } = LevelYear.First;
        public List<Book> Books { get; set; } = new List<Book>();
        public string? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public List<Fee> Fees { get; set; } = new List<Fee>();
    }
}
