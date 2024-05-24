using B_UniversityManagement.Models;

namespace B_UniversityManagement.IRepository
{
    public interface IProfessorRepo
    {
        void Create(User professor);
        void Update(User professor);
        void Delete(User professor);
        List<User> GetAll();
        User GetById(string id);
    }
}
