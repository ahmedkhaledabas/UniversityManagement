using B_UniversityManagement.Models;

namespace B_UniversityManagement.IRepository
{
    public interface IProfessorRepo
    {
        void Create(Professor professor);
        void Update(Professor professor);
        void Delete(Professor professor);
        List<Professor> GetAll();
        Professor GetById(string id);
    }
}
