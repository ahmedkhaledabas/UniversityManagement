using B_UniversityManagement.Models;

namespace B_UniversityManagement.IRepository
{
    public interface IStudentCourses
    {
        void Create(StudentCourse studentCourse);

        List<StudentCourse> GetAll();
    }
}
