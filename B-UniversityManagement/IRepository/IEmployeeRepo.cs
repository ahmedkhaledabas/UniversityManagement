using B_UniversityManagement.Models;

namespace B_UniversityManagement.IRepository
{
    public interface IEmployeeRepo
    {
        void Create(User employee);
        void Update(User employee);
        void Delete(User employee);
        List<User> GetAll();
        User GetById(string id);
    }
}
