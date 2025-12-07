using UsersAPI.Domain.Entitys;

namespace UsersAPI.Domain.ReposInterfaces
{
    public interface IUserRepository : Repository<User>
    {
        User FindByEmail(string email);
        User Login(string email, string password);
    }
}
