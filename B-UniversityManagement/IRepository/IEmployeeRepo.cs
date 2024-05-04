using B_UniversityManagement.Models;

namespace B_UniversityManagement.IRepository
{
    public interface IEmployeeRepo
    {
        void Create(Employee employee);
        void Update(Employee employee);
        void Delete(Employee employee);
        List<Employee> GetAll();
        Employee GetById(int id);
    }
}
