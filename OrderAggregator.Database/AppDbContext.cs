using Microsoft.EntityFrameworkCore;
using OrderAggregator.Model.Models;

namespace OrderAggregator.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Order> Orders { get; set; }
    }
}
