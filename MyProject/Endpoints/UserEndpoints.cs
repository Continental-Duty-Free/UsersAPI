using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyProject.Data.Repos.EF;
using System.Net;
using UsersAPI.AppLogic.UseCasesInterfaces.Users;
using UsersAPI.Domain.DTOs.Users;
using UsersAPI.Domain.Entitys;
using UsersAPI.Domain.EntitysExceptions;

namespace UsersAPI.Endpoints
{
    public static class UserEndpoints
    {
        public static void MapUsersEndPoints(this WebApplication app)
        {
            var userGroup = app.MapGroup("/api/User")
                .RequireAuthorization()
                .WithOpenApi();

            userGroup.MapPost("/login", Login)
                .WithName("Login")
                .WithSummary("Get authenticated in the system via Login")
                .AllowAnonymous(); 
        }

        public static IResult Login(
            [FromBody] LoginRequestDTO payload,
            ILogin login,
            Token tokenService,
            ILogger<Program> logger)
        {
            try
            { 
                var user = login.Run(payload.Email, payload.Password);
                string token = tokenService.GenerateToken(user);
                return Results.Ok(new
                {
                    message = "Login successfull!",
                    access_token = token,
                    name = user.Person.Name
                });
            }
            catch (UserException ex)
            {
                logger.LogWarning(ex, "User exception occurred: " + ex.Message);
                return Results.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception occurred: " + ex.Message);
                return Results.StatusCode(500);
            }
        }
    }
}
