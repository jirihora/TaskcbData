using Microsoft.EntityFrameworkCore;
using OrderAgregator.Model.Model;

namespace OrderAgregator.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Order> Orders { get; set; }
    }
}
