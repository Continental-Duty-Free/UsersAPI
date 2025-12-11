using UsersAPI.Domain.Entitys;

namespace UsersAPI.Domain.ReposInterfaces
{
    public interface IUserRepository : Repository<User>
    {
        Task<User> FindByEmail(string email);
        void CreatePerson(User user, Person person);
        Person GetPersonByUserId(int id);
    }
}
