//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using MyProject.AppLogic.UseCasesInterfaces.Users;
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
        private readonly Token tokenService;
        public UserController(ICreateUser createUser, ILogin login, Token tokenService)
        {
            this.createUser = createUser;
            this.login = login;
            this.tokenService = tokenService;
        }

        [HttpPost("Register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public IActionResult Register([FromBody] CreateUserDTO payload)
        {
            try
            {
                createUser.Run(payload);
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
                string accessToken = tokenService.GenerateToken(user);
                return Ok(new
                {
                    message = "Login successfull!",
                    token = accessToken
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
