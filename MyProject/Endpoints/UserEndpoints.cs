using Microsoft.AspNetCore.Mvc;
using MyProject.AppLogic.UseCasesInterfaces.Users;
using UsersAPI.AppLogic.UseCasesInterfaces.Users;
using UsersAPI.Domain.DTOs.Users;
using UsersAPI.Domain.Entitys;
using UsersAPI.Domain.EntitysExceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Runtime.CompilerServices;

namespace UsersAPI.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUsersEndPoints(this WebApplication app)
        {
            var userGroup = app.MapGroup("/api/User")
                .RequireAuthorization()
                .WithOpenApi();

            userGroup.MapPost("", Register)
                .WithName("Register")
                .WithSummary("Register a new user")
                .AllowAnonymous();
            userGroup.MapPost("/login", Login)
                .WithName("Login")
                .WithSummary("Get authenticated in the system via Login")
                .AllowAnonymous();
        }

        public static IResult Register(
            [FromBody] CreateUserDTO payload,
            ICreateUser createUser,
            ILogger<Program> logger)
        {
            try
            {
                createUser.Run(payload);
                return Results.Ok(new { message = "User registered successfully" });
            }
            catch (UserException ex)
            {
                logger.LogWarning(ex.Message);
                return Results.BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogError("An error ocurred while trying to create a new user " + ex.Message);
                return Results.BadRequest( new { error = ex.Message });
            }
        }

        public static async Task<IResult> Login(
            [FromBody] LoginRequestDTO payload,
            ILogin login,
            Token tokenService,
            ILogger<Program> logger)
        {
            try
            {
                var user = await login.Run(payload.Email, payload.Password);
                string accessToken = tokenService.GenerateToken(user);
                return Results.Ok(new
                {
                    message = "Login successfull!",
                    token = accessToken,
                    name = user.Person.Name
                });
            }
            catch (UserException ex)
            {
                logger.LogWarning(ex.Message);
                return Results.BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                logger.LogError("An error ocurred while trying to create a new user " + ex.Message);
                return Results.StatusCode(500);
            }
        }
    }
}
