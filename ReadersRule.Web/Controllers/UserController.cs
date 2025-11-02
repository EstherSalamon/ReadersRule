using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReadersRule.Data;
using ReadersRule.Web.Models;
using System.Security.Claims;

namespace ReadersRule.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly string _connectionString;

        public UserController(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("ConStr");
        }

        [HttpPost]
        [Route("signup")]
        public void SignUp(SignUpData data)
        {
            UserRepository repo = new UserRepository(_connectionString);
            repo.AddUser(data.User, data.Password);

            //throw / catch errors?
        }

        [HttpGet]
        [Route("checkemail")]
        public bool CheckEmailExists(string email)
        {
            UserRepository repo = new UserRepository(_connectionString);
            return repo.IfEmailExists(email);
        }

        [HttpPost]
        [Route("login")]
        public User Login(LogInData data)
        {
            UserRepository repo = new UserRepository(_connectionString);
            User user = repo.VerifyUser(data.Email, data.Password);
            
            if(user != null)
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email)
                };

                HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(claims, "cookies", ClaimTypes.Email, "role"))).Wait();
            }

            return user;
        }

        [HttpPost]
        [Route("logout")]
        public void LogOut()
        {
            HttpContext.SignOutAsync().Wait();
        }

        [HttpGet]
        [Route("currentuser")]
        public User GetCurrentUser()
        {
            if(!User.Identity.IsAuthenticated)
            {
                return null;
            }

            UserRepository repo = new UserRepository(_connectionString);
            return repo.GetUserByEmail(User.Identity.Name);
        }
    }
}
