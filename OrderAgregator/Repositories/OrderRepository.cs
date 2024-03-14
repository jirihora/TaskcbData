using Microsoft.EntityFrameworkCore;
using OrderAgregator.Database;
using OrderAgregator.Model.Model;
using OrderAgregator.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderAgregator.Repositories
{
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
