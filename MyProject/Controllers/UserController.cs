//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using MyProject.AppLogic.UseCasesInterfaces.Users;
using UsersAPI.AppLogic.UseCasesInterfaces.Token;
using UsersAPI.AppLogic.UseCasesInterfaces.Users;
using UsersAPI.Domain.DTOs.Users;
using UsersAPI.Domain.Entitys;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICreateUser createUser;
        private readonly ILogin login;
        private readonly IGenerateToken generateToken;

        public UserController(ICreateUser createUser, ILogin login, IGenerateToken generateToken)
        {
            this.createUser = createUser;
            this.login = login;
            this.generateToken = generateToken;
        }

        [HttpPost("Register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public IActionResult Register([FromBody] CreateUserDTO user)
        {
            try
            {
                createUser.Run(user);
                return Ok(new { message = "User registered successfully "});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost("Login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public IActionResult Login([FromBody] LoginRequestDTO payload)
        {
            try
            {
                User user = login.Run(payload.Email, payload.Password);
                if (user == null)
                    throw new Exception("Invalid credentials or user not found");
                string token = generateToken.Run(user);
                return Ok(new
                {
                    message = "Login successfull!",
                    user = new { user.Name},
                    token = token
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
