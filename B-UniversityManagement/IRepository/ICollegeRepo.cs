using B_UniversityManagement.Models;

namespace B_UniversityManagement.IRepository
{
    public interface ICollegeRepo
    {
        void Create(College college);
        void Update(College college);
        void Delete(College college);
        List<College> GetAll();
        College GetById(int id);
    }
}
