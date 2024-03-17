using Microsoft.EntityFrameworkCore;
using OrderAggregator.Model.Model;

namespace OrderAggregator.Database
{
    /// <summary>
    /// DB context class.
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<Order> Orders { get; set; }
    }
}
