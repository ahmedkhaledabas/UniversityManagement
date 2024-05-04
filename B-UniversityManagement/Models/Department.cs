namespace B_UniversityManagement.Models
{
    public class Department : BaseProperties
    {
        public string Name { get; set; } = null!;       
        public string? Description { get; set; }
        public List<Course> Courses { get; set; } = null!;
        public List<Student> Students { get; set; } = null!;
        public int CollegeId { get; set; }
        public College College { get; set; } = null!;
    }
}
