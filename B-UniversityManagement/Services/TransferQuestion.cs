using B_UniversityManagement.DTOs;
using B_UniversityManagement.Models;

namespace B_UniversityManagement.Services
{
    public static class TransferQuestion
    {
        public static Question DtoToQuestion (QuestionDTO questionDTO)
        {
            return new Question
            {
                Id = questionDTO.Id,
                Ques = questionDTO.Ques,
                Answer = questionDTO.Answer,
                Opt1 = questionDTO.Opt1,
                Opt2 = questionDTO.Opt2,
                Opt3 = questionDTO.Opt3,
                Opt4 = questionDTO.Opt4,
                QuizId = questionDTO.QuizId
            };
        }

        public static QuestionDTO QuestionToDTO(Question question)
        {
            return new QuestionDTO
            {
                Id = question.Id,
                Ques = question.Ques,
                Answer = question.Answer,
                Opt1 = question.Opt1,
                Opt2 = question.Opt2,
                Opt3 = question.Opt3,
                Opt4 = question.Opt4,
                QuizId = question.QuizId
            };
        }

        public static List<Question> DTOsToListQuestions (List<QuestionDTO> questionDTOs)
        {
            List<Question> questions = new List<Question>();
            foreach (var questionDTO in questionDTOs)
            {
                var question = DtoToQuestion(questionDTO);
                questions.Add(question);
            }
            return questions;
        }


        public static List<QuestionDTO> ListQuestionToDTOs(List<Question> questions)
        {
            List<QuestionDTO> questionDTOs = new List<QuestionDTO>();
            foreach (var question in questions)
            {
                var questionDTO = QuestionToDTO(question);
                questionDTOs.Add(questionDTO);
            }
            return questionDTOs;
        }
    }
}
