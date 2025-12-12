using Microsoft.EntityFrameworkCore;
using UsersAPI.Domain.Entitys;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyProject.Data.Repos.EF
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context>options):base(options) 
        {
        
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Discount> Discounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>()
                            .ToTable("Users");  

            modelBuilder.Entity<Customer>()
                .ToTable("Customers");  

            modelBuilder.Entity<Employee>()
                .ToTable("Employees");  

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
