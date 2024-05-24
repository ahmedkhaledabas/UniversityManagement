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
        public void Create(User student)
        {
            context.Users.Add(student);
            context.SaveChanges();
        }

        public void Delete(User student)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Update(User student)
        {
            throw new NotImplementedException();
        }
    }
}
