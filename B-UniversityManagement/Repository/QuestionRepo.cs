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

        public void ChangeStatus(string questionId)
        {
            var ques = GetById(questionId);
            context.SaveChanges();
        }

        public void Create(Question question)
        {
            context.Questions.Add(question);
            context.SaveChanges();
        }

        public List<Question> GetAllByQuizId(string quizId)
        {
            return context.Questions.Where(q=>q.QuizId == quizId).ToList();
        }

        public Question GetById(string id) => context.Questions.Find(id);

        public List<Question> GetQuestions(string profId)
        {
            var questions = context.Questions.ToList();
            return questions;
        }
    }
}
