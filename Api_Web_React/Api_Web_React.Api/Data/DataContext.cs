using Api_Web_React.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Web_React.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasIndex(u  => u.Id).IsUnique();
            modelBuilder.Entity<Customer>().HasIndex(c => c.Id).IsUnique();
        }
    }
}
