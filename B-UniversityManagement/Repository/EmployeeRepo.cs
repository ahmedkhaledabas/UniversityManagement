using B_UniversityManagement.Data;
using B_UniversityManagement.IRepository;
using B_UniversityManagement.Models;

namespace B_UniversityManagement.Repository
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly UniversityDbContext context;

        public EmployeeRepo(UniversityDbContext context)
        {
            this.context = context;
        }
        public void Create(User employee)
        {
            throw new NotImplementedException();
        }

        public void Delete(User employee)
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

        public void Update(User employee)
        {
            throw new NotImplementedException();
        }
    }
}
