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
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Person)
                .WithOne(p => p.User)
                .HasForeignKey<Person>(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
