using System.Runtime.CompilerServices;
using UsersAPI.Domain.DTOs.Users;
using UsersAPI.Domain.Entitys;

namespace MyProject.AppLogic.UseCasesInterfaces.Users
{
    public interface ICreateUser
    {
        void Run(CreateUserDTO user);
    }
}
