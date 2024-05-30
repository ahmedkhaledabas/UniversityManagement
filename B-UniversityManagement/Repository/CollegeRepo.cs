using B_UniversityManagement.Data;
using B_UniversityManagement.IRepository;
using B_UniversityManagement.Models;

namespace B_UniversityManagement.Repository
{
    public class CollegeRepo : ICollegeRepo
    {
        private readonly UniversityDbContext context;

        public CollegeRepo(UniversityDbContext context) {
            this.context = context;
        }
        public void Create(College college)
        {
            context.Add(college);
            context.SaveChanges();
        }

        public void Delete(College college)
        {
                context.Colleges.Remove(college);
                context.SaveChanges();
        }

        public List<College> GetAll() => context.Colleges.ToList();

        public College GetById(string id)
        {
            var collegeFinded = context.Colleges.Find(id);
            if (collegeFinded != null)
            {
                return collegeFinded;
            }
            throw new NullReferenceException();
        }

        public void Update(College college)
        {
            var collegeFind = GetById(college.Id);
            if(collegeFind != null)
            {
                collegeFind.Img = (college.Img != null) ? college.Img : collegeFind.Img;
                collegeFind.Name = college.Name;
                collegeFind.Description = college.Description;
                context.SaveChanges();
            }
            else throw new NullReferenceException();
        }
    }
}
