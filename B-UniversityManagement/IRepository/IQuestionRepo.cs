using B_UniversityManagement.Models;

namespace B_UniversityManagement.IRepository
{
    public interface IQuestionRepo
    {
        void Create(Question question);

        List<Question> GetQuestions(string profId);

        List<Question> GetAllByCourseId(string courseId);
        

    }
}
