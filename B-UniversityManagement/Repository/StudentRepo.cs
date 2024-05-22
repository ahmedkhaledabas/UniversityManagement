using B_UniversityManagement.Data;
using B_UniversityManagement.IRepository;
using B_UniversityManagement.Models;

namespace B_UniversityManagement.Repository
{
    public class StudentRepo : IStudentRepo
    {
        private readonly UniversityDbContext context;

        public StudentRepo(UniversityDbContext context)
        {
            this.context = context;
        }
        public void Create(Student student)
        {
            context.Users.Add(student);
            context.SaveChanges();
        }

        public void Delete(Student student)
        {
            throw new NotImplementedException();
        }

        public List<Student> GetAll()
        {
            throw new NotImplementedException();
        }

        public Student GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
