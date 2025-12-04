using MyProject.AppLogic.UseCasesInterfaces.Users;
using MyProject.BusinessLogic.Entitys;
using System.ComponentModel.DataAnnotations;
using UsersAPI.BusinessLogic.DTOs.Users;
using UsersAPI.BusinessLogic.ReposInterfaces;

namespace MyProject.AppLogic.UseCasesImplementation.Users
{
    public class CreateUser : ICreateUser
    {
        private IUserRepository repository;

        public CreateUser(IUserRepository repository)
        {
            this.repository = repository;
        }

        public void Run(CreateUserDTO user)
        {
            try
            {
                if (repository.FindByEmail(user.Email) != null)
                {
                    throw new Exception("Please be so kind of choosing another email");
                }
                repository.Create(new User()
                {
                    Email = user.Email,
                    Password = user.Password,
                    Name = user.Name,
                    Username = user.Username,
                    Phone = user.Phone
                });
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}