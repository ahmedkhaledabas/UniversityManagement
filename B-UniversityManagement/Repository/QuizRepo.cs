using B_UniversityManagement.Data;
using B_UniversityManagement.IRepository;
using B_UniversityManagement.Models;

namespace B_UniversityManagement.Repository
{
    public class QuizRepo : IQuizRepo
    {
        private readonly UniversityDbContext context;

        public QuizRepo(UniversityDbContext context)
        {
            this.context = context;
        }
        public void ChangeStatus(string quizId)
        {
            throw new NotImplementedException();
        }

        public void Create(Quiz quiz)
        {
            context.Quizzes.Add(quiz);
            context.SaveChanges();
        }


        public List<Quiz> GetQuizes(string corseId) => context.Quizzes.Where(q => q.CourseId == corseId).ToList();
    }
}
