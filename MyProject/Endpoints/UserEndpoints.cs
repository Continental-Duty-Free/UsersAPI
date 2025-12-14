using Microsoft.AspNetCore.Mvc;
using UsersAPI.Domain.DTOs.Users;
using UsersAPI.Domain.Entitys;
using UsersAPI.AppLogic.UseCasesInterfaces.Users;

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
            var user =  login.Run(payload.Email, payload.Password);
            string accessToken = tokenService.GenerateToken(user);
            return Results.Ok(new
                {
                    message = "Login successfull!",
                    token = accessToken,
                    name = user.Person.Name
                });
        }
    }
}
