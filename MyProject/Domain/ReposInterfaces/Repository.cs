namespace UsersAPI.Domain.ReposInterfaces
{
    public interface Repository<T>
    {
        T Create (T entity);
        void Remove(int id);
        void Update (T entity);
        T GetById (int id);
        IQueryable<T> GetAll();
    }
}
