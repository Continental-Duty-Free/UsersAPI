using Microsoft.AspNetCore.Mvc;
using UsersAPI.AppLogic.UseCasesInterfaces.Customers;
using UsersAPI.Domain.DTOs.Customers;

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
            createUser.Run(payload);
            return Results.Created();
        }
    }
}
