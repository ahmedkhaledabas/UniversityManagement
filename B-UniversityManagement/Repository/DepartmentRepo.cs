using B_UniversityManagement.Data;
using B_UniversityManagement.IRepository;
using B_UniversityManagement.Models;

namespace B_UniversityManagement.Repository
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly UniversityDbContext context;

        public DepartmentRepo(UniversityDbContext context)
        {
            this.context = context;
        }

        public void Create(Department department)
        {
            context.Add(department);
            context.SaveChanges();
        }

        public void Update(Department department)
        {
            context.Update(department);
            context.SaveChanges();
        }

        public void Delete(Department department)
        {
            context.Remove(department);
            context.SaveChanges();
        }

        public List<Department> GetAll() => context.Departments.ToList();

        public Department GetById(int id)
        {
            return context.Departments.Find(id);
        }
    }
}
