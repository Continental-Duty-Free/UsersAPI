using Microsoft.AspNetCore.Identity;
using UsersAPI.AppLogic.UseCasesInterfaces.Users;
using UsersAPI.Domain.DTOs.Users;
using UsersAPI.Domain.Entitys;
using UsersAPI.Domain.EntitysExceptions;
using UsersAPI.Domain.Mappers;
using UsersAPI.Domain.ReposInterfaces;

namespace UsersAPI.AppLogic.UseCasesImplementation.Users
{
    public class Login : ILogin
    {
        public readonly IUserRepository repository;
        private readonly PasswordHasher<Person> passwordHasher;

        public Login(IUserRepository repository)
        {
            this.repository = repository;
            this.passwordHasher = new PasswordHasher<Person>();
        }

        public User Run(string email, string password)
        {
            User user = repository.FindByEmail(email);
            if (user == null)
                throw new UserException("Invalid credentials or user not found");

            Person person = repository.GetPersonByUserId(user.Id);
            var result = passwordHasher.VerifyHashedPassword(person, person.Password, password);
            if (result != PasswordVerificationResult.Success)
                throw new UserException("Invalid credentials or user not found");

            return user;
        }
    }
}
