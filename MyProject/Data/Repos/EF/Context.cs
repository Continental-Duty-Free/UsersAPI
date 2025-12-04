using Microsoft.EntityFrameworkCore;
using MyProject.BusinessLogic.Entitys;

namespace MyProject.Data.Repos.EF
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context>options):base(options) 
        {
        
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}
