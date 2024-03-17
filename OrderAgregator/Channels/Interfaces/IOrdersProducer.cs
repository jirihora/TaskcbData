using OrderAggregator.Model.Model;
using System.Threading.Tasks;

namespace OrderAggregator.Channels.Interfaces
{
    /// <summary>
    /// Bundles methods that are used to produce <see cref="OrdersMessage"/>.
    /// </summary>
    public interface IOrdersProducer
    {
        public Task SendOrdersMessageAsync(OrdersMessage ordersMessage);
    }
}
