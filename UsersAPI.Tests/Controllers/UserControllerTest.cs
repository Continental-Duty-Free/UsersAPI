using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyProject.AppLogic.UseCasesImplementation.Users;
using MyProject.AppLogic.UseCasesInterfaces.Users;
using MyProject.Controllers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersAPI.AppLogic.UseCasesImplementation.Users;
using UsersAPI.AppLogic.UseCasesInterfaces.Users;
using UsersAPI.Domain.DTOs.Users;
using UsersAPI.Domain.Entitys;
using UsersAPI.Domain.ReposInterfaces;
using Xunit;

//https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices
namespace UsersAPI.Tests.Controllers
{
    public class UserControllerTest
    {
        private readonly IUserRepository UserRepository;
        private readonly ILogger Logger;
        private readonly ILogin Login;
        private readonly ICreateUser CreateUser;
        private readonly Token TokenService;

        public UserControllerTest() 
        {
            this.UserRepository = A.Fake<IUserRepository>();
            this.Logger = A.Fake<ILogger>();
            this.Login = A.Fake<ILogin>();
            this.CreateUser = A.Fake<ICreateUser>();
            this.TokenService = A.Fake<Token>();
        }

        [Fact]
        public void UserController_Register_ReturnOk()
        {
            //Arrange
            var user = new CreateUserDTO()
            {
                Email = "test@gmail.com",
                Name = "Test",
                LastName = "User",
                Password = "Password123",
                PasswordConfirmation = "Password123"
            };
            A.CallTo(() => CreateUser.Run(user)).DoesNothing();
            var controller = new UserController(CreateUser, Login, TokenService, Logger);

            //Act
            var result = controller.Register(user);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(OkObjectResult));

            var response = result as OkObjectResult;
            var messageProperty = response.Value.GetType().GetProperty("message");
            var messageValue = messageProperty.GetValue(response.Value) as string;

            Assert.Equal("User registered successfully", messageValue); 
        }

    }
}