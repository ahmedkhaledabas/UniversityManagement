using B_UniversityManagement.Enums;
using B_UniversityManagement.Models;

namespace B_UniversityManagement.DTOs
{
    public class QuizDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public List<QuestionDTO> QuestionDTOs { get; set; }
        public Status Status { get; set; } = Status.open;
        public string CourseId { get; set; }
    }
}
