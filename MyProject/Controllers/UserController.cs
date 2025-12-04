//using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using MyProject.AppLogic.UseCasesInterfaces.Users;
using UsersAPI.BusinessLogic.DTOs.Users;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICreateUser createUser;

        public UserController(ICreateUser createUser)
        {
            this.createUser = createUser;
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
    }
}
