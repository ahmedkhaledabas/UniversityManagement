using B_UniversityManagement.DTOs;
using B_UniversityManagement.Models;
using B_UniversityManagement.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace B_UniversityManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(userLoginDTO.UserName);
                if (user != null)
                {
                    var checkPass = await userManager.CheckPasswordAsync(user, userLoginDTO.Password);
                    if (checkPass)
                    {
                        await signInManager.SignInAsync(user, userLoginDTO.RememberMe);
                        return Ok( new {message = "Success Login" , role = userManager.GetRolesAsync(user).Result , userName = userLoginDTO.UserName} );
                    }
                    ModelState.AddModelError(string.Empty, "Invalid UserName Or Password");
                }
                return NotFound(new { message = "Invalid UserName Or Password" });
            }
            ModelState.AddModelError(string.Empty, "Invalid UserName Or Password");
            return NotFound(new { message = "Invalid UserName Or Password" });
        }


        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return Ok();
        }

        [HttpGet("userName")]
        public async Task<IActionResult> GetUser(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            if (user != null)
            {
                    return Ok(user);
               
            }
            return NotFound(new { message = "Invalid" });
        }

        [HttpDelete]

        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded) return Ok();
                else return BadRequest();
            }
            else return NotFound();
        }
    }
}
