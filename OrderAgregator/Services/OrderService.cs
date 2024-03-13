using OrderAgregator.Channels.Interfaces;
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
        private readonly IOrdersProducer _orderProducer;

        public OrderService(IOrdersProducer orderProducer)
        {
            _orderProducer = orderProducer;
        }

        public async Task<IEnumerable<Order>> UploadOrders(Order[] orders)
        {
            LogOrders(orders);

            await _orderProducer.SendOrdersMessageAsync(new OrdersMessage {Orders = orders});

            return orders.ToList();
        }

        private static void LogOrders(Order[] orders)
        {
            foreach (Order order in orders)
            {
                Log.Information($"Order: product id: {order.ProductId}, quantity: {order.Quantity}.");
            }
        }
    }
}
