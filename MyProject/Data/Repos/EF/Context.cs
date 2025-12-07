using Microsoft.EntityFrameworkCore;
using UsersAPI.Domain.Entitys;

namespace MyProject.Data.Repos.EF
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context>options):base(options) 
        {
        
        }

        public DbSet<User> Users { get; set; }
    }
}
