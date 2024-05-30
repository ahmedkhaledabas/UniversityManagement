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
        public string? Course {  get; set; }
        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

        public AcademicRank? Rank { get; set; }

        //student
        public LevelYear? levelYear { get; set; }
        public string? Book {  get; set; }
        public ICollection<StudentBook> StudentBooks { get; set; } = new List<StudentBook>();
        public string? DepartmentId { get; set; }
        public Department? Department { get; set; }
        public List<Fee>? Fees { get; set; }
    }
}
