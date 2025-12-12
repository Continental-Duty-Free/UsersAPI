using UsersAPI.Domain.DTOs.Users;
using UsersAPI.Domain.Entitys;

namespace UsersAPI.AppLogic.UseCasesInterfaces.Users
{
    public interface ILogin
    {
        User Run(string email, string password);
    }
}
