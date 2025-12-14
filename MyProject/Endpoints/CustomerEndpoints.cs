using Microsoft.AspNetCore.Mvc;
using UsersAPI.AppLogic.UseCasesInterfaces.Customers;
using UsersAPI.Domain.DTOs.Customers;
using UsersAPI.Domain.EntitysExceptions;

namespace UsersAPI.Endpoints
{
    public static class CustomerEndpoints
    {
        public static void MapCustomersEndPoints(this WebApplication app)
        {
            var userGroup = app.MapGroup("/api/Customer")
                .RequireAuthorization()
                .WithOpenApi();

            userGroup.MapPost("", Register)
                .WithName("")
                .WithSummary("Register a new customer")
                .AllowAnonymous();
        }

        public static IResult Register(
            [FromBody] CreateCustomerDTO payload,
            ICreateCustomer createUser,
            ILogger<Program> logger)
        {
            try
            {
                createUser.Run(payload);
                return Results.Created();
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
