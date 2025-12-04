using UsersAPI.BusinessLogic.DTOs.Users;

namespace MyProject.AppLogic.UseCasesInterfaces.Users
{
    public interface ICreateUser
    {
        void Run(CreateUserDTO user);
    }
}
