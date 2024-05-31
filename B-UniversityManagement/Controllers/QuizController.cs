using B_UniversityManagement.DTOs;
using B_UniversityManagement.IRepository;
using B_UniversityManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Utilities;

namespace B_UniversityManagement.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuestionRepo questionRepo;
        private readonly UserManager<User> userManager;
        private readonly IStudentCourses studentCoursesRepo;

        public QuizController(IQuestionRepo questionRepo , UserManager<User> userManager , IStudentCourses studentCoursesRepo)
        {
            this.questionRepo = questionRepo;
            this.userManager = userManager;
            this.studentCoursesRepo = studentCoursesRepo;
        }

        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestion(Question questionDTO)
        {
            if (ModelState.IsValid)
            {
                var prof = await userManager.FindByNameAsync(questionDTO.ProfessorId);
                if (prof != null)
                {
                    questionDTO.ProfessorId = prof.Id;
                }
                questionRepo.Create(questionDTO);
                return Ok();
            }
            else return BadRequest(ModelState);
        }


        [HttpGet("{userName}")]
        public async Task<ActionResult<Question>> GetQuestions(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var studentCourses = studentCoursesRepo.GetAllForStudent(user.Id);
                if (studentCourses.Count > 0)
                {
                    List<Question> questions = new List<Question>();
                    foreach (var item in studentCourses)
                    {
                        var q = questionRepo.GetAllByCourseId(item.CourseId);
                        if (q.Count > 0)
                        {
                            foreach (var question in q)
                            {
                                questions.Add(question);
                            }
                        }
                        else continue;
                    }
                    return Ok(questions);
                }
                else return NotFound();
            }
            else return BadRequest();
        }
    }
}
