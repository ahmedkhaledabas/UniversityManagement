namespace B_UniversityManagement.Models
{
    public class Department : BaseProperties
    {
        public string Name { get; set; } = null!;       
        public string? Description { get; set; }
        public List<Course> Courses { get; set; } = new List<Course> { };
        public List<User> Students { get; set; } = new List<User> { };
        public string CollegeId { get; set; } = null!;
        public College College { get; set; } = null!;
    }
}
