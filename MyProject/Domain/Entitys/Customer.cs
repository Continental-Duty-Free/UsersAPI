namespace UsersAPI.Domain.Entitys
{
    public class Customer : User
    {
        public ICollection<Product> FavouriteProducts { get; set; }
    }
}
