using B_UniversityManagement.Models;

namespace B_UniversityManagement.IRepository
{
    public interface IQuestionRepo
    {
        void Create(Question question);

        Question GetById(string id);

        void ChangeStatus(string questionId);

        List<Question> GetQuestions(string profId);

        List<Question> GetAllByQuizId(string quizId);
        

    }
}
