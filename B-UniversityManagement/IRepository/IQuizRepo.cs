using B_UniversityManagement.Models;

namespace B_UniversityManagement.IRepository
{
    public interface IQuizRepo
    {
        void Create(Quiz quiz);

        void ChangeStatus(string quizId);

        List<Quiz> GetQuizes(string corseId);

    }
}
