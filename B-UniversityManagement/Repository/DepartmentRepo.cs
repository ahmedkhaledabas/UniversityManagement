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
            context.Departments.Add(department);
            context.SaveChanges();
        }

        public void Update(Department department)
        {
            context.Departments.Update(department);
            context.SaveChanges();
        }

        public void Delete(string id)
        {
            Department department = GetById(id);
            context.Departments.Remove(department);
            context.SaveChanges();
        }

        public List<Department> GetAll() => context.Departments.ToList();

        public Department GetById(string id)
        {
            Department department = context.Departments.Find(id);
            if(department != null)
            {
                return department;
            }
            throw new NullReferenceException();
        }
    }
}
