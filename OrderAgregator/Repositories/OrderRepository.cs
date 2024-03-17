using Microsoft.EntityFrameworkCore;
using OrderAggregator.Database;
using OrderAggregator.Model.Model;
using OrderAggregator.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderAggregator.Repositories
{
    /// <summary>
    /// Implementation of <see cref="IOrderRepository"/> using in memory DB.
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _appDbContext;

        public OrderRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<int> SaveOrdersAsync(IEnumerable<Order> orders)
        {
            foreach (var order in orders)
            {
                _appDbContext.Orders.Add(order);
            }

            return await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _appDbContext.Orders.AsNoTracking().ToListAsync();
        }
    }
}
