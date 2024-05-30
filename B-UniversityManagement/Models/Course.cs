using B_UniversityManagement.Enums;
using Microsoft.Build.Framework;

namespace B_UniversityManagement.Models
{
    public class Course : BaseProperties
    {
        public LevelYear LevelYear { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Img { get; set; }
       // public List<User> Users { get; set; } = null!;
        public string UserId { get; set; } = null!;
        //public User Professor { get; set; } = null!;
        public string DepartmentId { get; set; } = null!;
        public Department? Department { get; set; }

        public ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
    }
}
