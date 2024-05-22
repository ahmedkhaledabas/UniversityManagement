using B_UniversityManagement.Models;

namespace B_UniversityManagement.IRepository
{
    public interface IStudentRepo
    {
        void Create(Student student);
        void Update(Student student);
        void Delete(Student student);
        List<Student> GetAll();
        Student GetById(string id);
    }
}
