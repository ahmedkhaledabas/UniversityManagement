using B_UniversityManagement.DTOs;
using B_UniversityManagement.IRepository;
using B_UniversityManagement.Models;
using B_UniversityManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
        private readonly IQuizRepo quizRepo;

        public QuizController(IQuestionRepo questionRepo, UserManager<User> userManager, IStudentCourses studentCoursesRepo, IQuizRepo quizRepo)
        {
            this.questionRepo = questionRepo;
            this.userManager = userManager;
            this.studentCoursesRepo = studentCoursesRepo;
            this.quizRepo = quizRepo;
        }

        [HttpPost]
        public async Task<ActionResult<List<QuestionDTO>>> PostQuiz(QuizDTO quizDTO)
        {
            if (ModelState.IsValid)
            {
                // 1- save all question in question table , 2- save quize direct
                var quiz = TransferQuiz.DtoToQuiz(quizDTO);
                quizRepo.Create(quiz);
                return Ok(quizDTO);

            }
            else return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<List<QuizDTO>>> GetQuizs(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var quizDTOs = new List<QuizDTO>();
                var roles = userManager.GetRolesAsync(user).Result;
                foreach (var role in roles)
                {
                    if(role == "Student")
                    {
                        var studentCourses = studentCoursesRepo.GetAllForStudent(user.Id);
                        foreach (var course in studentCourses)
                        {
                            var quizes = quizRepo.GetQuizes(course.CourseId);
                            if (quizes.Count > 0)
                            {
                                foreach (var quiz in quizes)
                                {
                                    quiz.Questions = questionRepo.GetAllByQuizId(quiz.Id);
                                    var quizDto = TransferQuiz.QuizToDTO(quiz);
                                    quizDTOs.Add(quizDto);
                                    
                                    //returnQuizes.Add(quiz);
                                }
                            }
                        }
                        
                    }
                    //role == Prof
                    else
                    {
                        
                    } 
                }
                
                return Ok(quizDTOs);
            }
            else return NotFound();
        }

        //[HttpPost]
        //public async Task<ActionResult<Question>> PostQuestion(Question questionDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var prof = await userManager.FindByNameAsync(questionDTO.ProfessorId);
        //        if (prof != null)
        //        {
        //            questionDTO.ProfessorId = prof.Id;
        //        }
        //        questionRepo.Create(questionDTO);
        //        return Ok();
        //    }
        //    else return BadRequest(ModelState);
        //}


        //[HttpGet]
        //public async Task<ActionResult<Question>> ChangeStatus(string questionId)
        //{
        //    questionRepo.ChangeStatus(questionId);
        //    return Ok();
        //}

        
    }
}
