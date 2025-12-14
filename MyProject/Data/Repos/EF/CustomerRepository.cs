using MyProject.Data.Repos.EF;
using UsersAPI.Domain.Entitys;
using UsersAPI.Domain.ReposInterfaces;

namespace UsersAPI.Data.Repos.EF
{
    public class CustomerRepository : ICustomerRepository
    {
        private Context db;

        public CustomerRepository(Context db)
        {
            this.db = db;
        }

        public Customer Create(Customer entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity;
        }

        public IQueryable<Customer> GetAll()
        {
            throw new NotImplementedException();
        }

        public Customer GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
