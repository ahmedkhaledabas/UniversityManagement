using B_UniversityManagement.Data;
using B_UniversityManagement.IRepository;
using B_UniversityManagement.Models;

namespace B_UniversityManagement.Repository
{
    public class QuestionRepo : IQuestionRepo
    {
        private readonly UniversityDbContext context;

        public QuestionRepo(UniversityDbContext context)
        {
            this.context = context;
        }
        public void Create(Question question)
        {
            context.Questions.Add(question);
            context.SaveChanges();
        }

        public List<Question> GetAllByCourseId(string courseId)
        {
            return context.Questions.Where(q => q.CourseId == courseId).ToList();
        }

        public List<Question> GetQuestions(string profId)
        {
            var questions = context.Questions.Where(q=>q.ProfessorId == profId).ToList();
            return questions;
        }
    }
}
