using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyProject.AppLogic.UseCasesImplementation.Users;
using MyProject.AppLogic.UseCasesInterfaces.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersAPI.AppLogic.UseCasesInterfaces.Users;
using UsersAPI.Domain.DTOs.Users;
using UsersAPI.Domain.Entitys;
using UsersAPI.Domain.ReposInterfaces;
using UsersAPI.Endpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace UsersAPI.Tests.Endpoints
{
    public class UserEndpointsTests
    {
        private readonly IUserRepository UserRepository;
        private readonly ILogger<Program> Logger;
        private readonly ILogin Login;
        private readonly ICreateUser CreateUser;
        private readonly Token TokenService;

        public UserEndpointsTests()
        {
            this.UserRepository = A.Fake<IUserRepository>();
            this.Logger = A.Fake<ILogger<Program>>();
            this.Login = A.Fake<ILogin>();
            this.CreateUser = A.Fake<ICreateUser>();
            this.TokenService = A.Fake<Token>();
        }

        [Fact]
        public void Register_Ok()
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

            //Act
            var response = UserEndpoints.Register(user, CreateUser, Logger);

            //Assert         
            response.Should().NotBeNull();
            response.Should().BeOfType(typeof(Ok));

            var result = response as Ok;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public void Login_Ok()
        {

        }
    }
}
