using B_UniversityManagement.IRepository;
using B_UniversityManagement.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace B_UniversityManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepo courseRepo;

        public CourseController(ICourseRepo courseRepo)
        {
            this.courseRepo = courseRepo;
        }
    }
}
