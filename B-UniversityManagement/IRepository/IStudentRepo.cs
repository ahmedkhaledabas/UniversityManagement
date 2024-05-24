using B_UniversityManagement.Models;

namespace B_UniversityManagement.IRepository
{
    public interface IStudentRepo
    {
        void Create(User student);
        void Update(User student);
        void Delete(User student);
        List<User> GetAll();
        User GetById(string id);
    }
}
