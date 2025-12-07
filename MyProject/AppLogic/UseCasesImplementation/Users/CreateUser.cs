using MyProject.AppLogic.UseCasesInterfaces.Users;
using System.ComponentModel.DataAnnotations;
using UsersAPI.Domain.DTOs.Users;
using UsersAPI.Domain.ReposInterfaces;
using UsersAPI.Domain.Entitys;
using Microsoft.AspNetCore.Identity;

namespace MyProject.AppLogic.UseCasesImplementation.Users
{
    public class CreateUser : ICreateUser
    {
        private IUserRepository repository;
        private readonly PasswordHasher<User> passwordHasher;

        public CreateUser(IUserRepository repository)
        {
            this.repository = repository;
            this.passwordHasher = new PasswordHasher<User>();
        }

        public void Run(CreateUserDTO user)
        {
            if (repository.FindByEmail(user.Email) != null)
                throw new Exception("Please be so kind of choosing another email");
            var newUser = new User()
            {
                Email = user.Email,
                Password = user.Password,
                Name = user.Name,
                Username = user.Username
            };
            user.Password = passwordHasher.HashPassword(newUser, user.Password);
            repository.Create(newUser);
        }
    }
}