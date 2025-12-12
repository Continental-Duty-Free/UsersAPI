using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public User Create(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity;
        }

        public void CreatePerson(User user, Person person)
        {
            db.Persons.Add(person);
            db.SaveChanges();
        }

        public User FindByEmail(string email)
        {
            return db.Users
                .Include(user => user.Person)
                .FirstOrDefault(user => user.Email == email);
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Person GetPersonByUserId(int id)
        {
            return db.Persons
                .Where(person => person.UserId == id)
                .FirstOrDefault();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        IQueryable<User> Repository<User>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
