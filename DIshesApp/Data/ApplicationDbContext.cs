using DIshesApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace DIshesApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Dish> Dishes { get; set; }
    }
}
