using B_UniversityManagement.DTOs;
using B_UniversityManagement.Models;

namespace B_UniversityManagement.Services
{
    public static class TransferQuiz
    {
        public static Quiz DtoToQuiz(QuizDTO quizDTO)
        {
            return new Quiz
            {
                Id = quizDTO.Id,
                Name = quizDTO.Name,
                Description = quizDTO.Description,
                Questions = TransferQuestion.DTOsToListQuestions(quizDTO.QuestionDTOs),
                CourseId = quizDTO.CourseId,
                Status = quizDTO.Status
            };
        }

        public static QuizDTO QuizToDTO(Quiz quiz)
        {
            return new QuizDTO
            {
                Id = quiz.Id,
                Name = quiz.Name,
                Description = quiz.Description,
                QuestionDTOs = TransferQuestion.ListQuestionToDTOs(quiz.Questions),
                CourseId = quiz.CourseId,
                Status = quiz.Status
            };
        }
    }
}
