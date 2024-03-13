using OrderAgregator.Model.Model;
using System.Threading.Tasks;

namespace OrderAgregator.Channels.Interfaces
{
    public interface IOrdersProducer
    {
        public Task SendOrdersMessageAsync(OrdersMessage ordersMessage);
    }
}
