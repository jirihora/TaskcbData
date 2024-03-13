using OrderAgregator.Model.Model;
using OrderAgregator.Services.Interfaces;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderAgregator.Services
{
    public class OrderService : IOrderService
    {
        public async Task<IEnumerable<Order>> UploadOrders(Order[] orders)
        {
            foreach (Order order in orders)
            {
                Log.Information(OrderToString(order));
            }

            return orders.ToList();
        }

        private static string OrderToString(Order order)
        {
            return $"Order: product id: {order.ProductId}, quantity: {order.Quantity}.";
        }
    }
}
