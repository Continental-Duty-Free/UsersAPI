using UsersAPI.AppLogic.UseCasesInterfaces.Users;
using UsersAPI.Domain.DTOs.Users;
using UsersAPI.Domain.Entitys;
using UsersAPI.Domain.Mappers;
using UsersAPI.Domain.ReposInterfaces;

namespace UsersAPI.AppLogic.UseCasesImplementation.Users
{
    public class Login : ILogin
    {
        public readonly IUserRepository repository;

        public Login(IUserRepository repository)
        {
            this.repository = repository;
        }

        public User Run(string email, string password)
        {
            User user = repository.Login(email, password);
            return user;
        }
    }
}
