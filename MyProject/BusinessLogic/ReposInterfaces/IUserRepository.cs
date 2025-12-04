using MyProject.BusinessLogic.Entitys;
using MyProject.BusinessLogic.ReposInterfaces;

namespace UsersAPI.BusinessLogic.ReposInterfaces
{
    public interface IUserRepository : Repository<User>
    {
        User FindByEmail(string email);
    }
}
