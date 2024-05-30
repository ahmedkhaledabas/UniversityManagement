using B_UniversityManagement.Data;
using B_UniversityManagement.IRepository;
using B_UniversityManagement.Models;

namespace B_UniversityManagement.Repository
{
    public class CourseRepo : ICourseRepo
    {
        private readonly UniversityDbContext context;

        public CourseRepo(UniversityDbContext context)
        {
            this.context = context;
        }
        public void Create(Course course)
        {
            context.Courses.Add(course);
            context.SaveChanges();
        }

        public void Delete(Course course)
        {
            context.Courses.Remove(course);
            context.SaveChanges();
        }

        public List<Course> GetAll() => context.Courses.ToList();

        public Course GetById(string id)
        {
            var course = context.Courses.Find(id);
            if(course != null)
            {
                return course;
            }
            throw new NullReferenceException();
        }

        public void Update(Course course)
        {
            var getCourse = GetById(course.Id);
            if (getCourse != null)
            {
                getCourse.Img = course.Img != null ? course.Img : getCourse.Img;
                getCourse.Name = course.Name;
                getCourse.DepartmentId = course.DepartmentId;
                getCourse.Description = course.Description;
                getCourse.LevelYear = course.LevelYear;
                getCourse.UserId = course.UserId;
                context.SaveChanges();
            }
            else throw new NullReferenceException();
        }
    }
}
