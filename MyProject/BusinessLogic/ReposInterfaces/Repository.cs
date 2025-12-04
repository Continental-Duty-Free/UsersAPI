namespace MyProject.BusinessLogic.ReposInterfaces
{
    public interface Repository<T>
    {
        void Create (T entity);
        void Remove(int id);
        void Update (T entity);
        T GetById (int id);
        IEnumerable<T> GetAll();
    }
}
