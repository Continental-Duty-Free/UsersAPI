using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly Token TokenService;

        public UserEndpointsTests()
        {
            this.UserRepository = A.Fake<IUserRepository>();
            this.Logger = A.Fake<ILogger<Program>>();
            this.Login = A.Fake<ILogin>();
            this.TokenService = A.Fake<Token>();
        }

        [Fact]
        public void Login_Ok()
        {

        }
    }
}
