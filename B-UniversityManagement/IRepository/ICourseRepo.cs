using B_UniversityManagement.Models;

namespace B_UniversityManagement.IRepository
{
    public interface ICourseRepo
    {
        void Create(Course course);
        void Update(Course course);
        void Delete(Course course);
        List<Course> GetAll();
        Course GetById(string id);
    }
}
