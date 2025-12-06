using MyProject.Data.Repos.EF;
using UsersAPI.Domain.Entitys;
using UsersAPI.Domain.ReposInterfaces;

namespace UsersAPI.Data.Repos.EF
{
    public class UserRepository : IUserRepository
    {
        private Context db;

        public UserRepository(Context db)
        {
            this.db = db;
        }

        public void Create(User entity)
        {
            try
            {
                db.Users.Add(entity);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public User FindByEmail(string email)
        {
            try
            {
                User user = db.Users
                    .Where(user => user.Email == email)
                    .FirstOrDefault();
                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
