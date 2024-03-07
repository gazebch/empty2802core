using empty2802core.Entities;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace empty2802core
{
    public class DBContext : DbContext
    {
        public DbSet<Basket> Basket { get; set; } = null!;
        public DbSet<Products> Products { get; set; } = null!;
        public DbSet<Users> Users { get; set; } = null!;
        public DBContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=test2802;Username=postgres;Password=1");
        }
    }
}
