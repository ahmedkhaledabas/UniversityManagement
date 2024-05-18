using B_UniversityManagement.Enums;
using System.Reflection;

namespace B_UniversityManagement.Models
{
    public class Student : User
    {
        public string Code { get; set; } = null!;
        public LevelYear levelYear { get; set; } = LevelYear.First;
        public List<Course> Courses { get; set; } = null!;
        public List<Book>? Books { get; set; } = new List<Book>();
        public int DepartmentId { get; set; }
        public Department Department { get; set; } = null!;
        public List<Fee>? Fees { get; set; } = new List<Fee>();
    }
}
