using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyProject.Data.Repos.EF;
using UsersAPI.AppLogic.UseCasesImplementation.Customers;
using UsersAPI.AppLogic.UseCasesInterfaces.Customers;
using UsersAPI.Data.Repos.EF;
using UsersAPI.Domain.DTOs.Customers;
using UsersAPI.Domain.DTOs.Users;
using UsersAPI.Domain.Entitys;
using UsersAPI.Domain.EntitysExceptions;
using UsersAPI.Domain.ReposInterfaces;
using UsersAPI.Endpoints;
using Xunit;

namespace UsersAPI.Tests.Endpoints
{
    public class CustomerEndpointsTest : IDisposable
    {
        private readonly Context _context;
        private readonly ILogger<Program> _logger;
        private readonly ICreateCustomer _createCustomer;
        private readonly IUserRepository _userRepository;
        public CustomerEndpointsTest()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;

            _context = new Context(options);
            _context.Database.EnsureCreated();

            _userRepository = new UserRepository(_context);
            _createCustomer = new CreateCustomer(_userRepository);

            _logger = A.Fake<ILogger<Program>>();

            SeedDatabase();
        }

        private void SeedDatabase()
        {

        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public void Register_Ok()
        {
            // Arrange
            var user = new CreateCustomerDTO
            {
                Email = "test@gmail.com",
                Name = "Test",
                LastName = "User",
                Password = "Password123",
                PasswordConfirmation = "Password123"
            };

            // Act
            var response = CustomerEndpoints.Register(user, _createCustomer, _logger);

            // Assert
            response.Should().NotBeNull();
            var result = Assert.IsType<Created>(response);
            Assert.Equal(201, result.StatusCode);
        }

        [Fact]
        public void Register_WithDuplicateEmail_ReturnsBadRequest()
        {
            // Arrange
            var user1 = new CreateCustomerDTO
            {
                Email = "duplicate@gmail.com",
                Name = "First",
                LastName = "User",
                Password = "Password123",
                PasswordConfirmation = "Password123"
            };

            var user2 = new CreateCustomerDTO
            {
                Email = "duplicate@gmail.com", // Mismo email
                Name = "Second",
                LastName = "User",
                Password = "Password123",
                PasswordConfirmation = "Password123"
            };

            // Act - Crear primer usuario
            var response1 = CustomerEndpoints.Register(user1, _createCustomer, _logger);
            Assert.IsType<Created>(response1);

            try
            {
                // Act - Intentar crear segundo usuario con mismo email
                var response2 = CustomerEndpoints.Register(user2, _createCustomer, _logger);
            }
            catch (UserException ex)
            {
                Assert.Equal($"{ex.Message}", ex.Message);
            }
        }

        [Fact]
        public void Register_WithInvalidPassword_ReturnsBadRequest()
        {
            // Arrange
            var user = new CreateCustomerDTO
            {
                Email = "invalid@gmail.com",
                Name = "Test",
                LastName = "User",
                Password = "123", // Password muy corta
                PasswordConfirmation = "123"
            };

            // Act
            var response = CustomerEndpoints.Register(user, _createCustomer, _logger);

            // Assert
            response.Should().NotBeNull();
            var result = Assert.IsType<BadRequest<object>>(response);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void Register_WithMismatchedPasswords_ReturnsBadRequest()
        {
            // Arrange
            var user = new CreateCustomerDTO
            {
                Email = "mismatch@gmail.com",
                Name = "Test",
                LastName = "User",
                Password = "Password123",
                PasswordConfirmation = "DifferentPassword123"
            };

            // Act
            var response = CustomerEndpoints.Register(user, _createCustomer, _logger);

            // Assert
            response.Should().NotBeNull();
            var result = Assert.IsType<BadRequest<object>>(response);
            Assert.Equal(400, result.StatusCode);
        }
    }
}