using B_UniversityManagement.Enums;

namespace B_UniversityManagement.Models
{
    public class Quiz
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public List<Question> Questions { get; set; }

        //public string CourseId { get; set; }
        public Status Status { get; set; } = Status.open;

        public Course Course {  get; set; }
        public string CourseId { get; set; }
    }
}
