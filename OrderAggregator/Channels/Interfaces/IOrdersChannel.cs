using OrderAggregator.Model.Models;
using System.Threading.Channels;

namespace OrderAggregator.Channels.Interfaces
{
    /// <summary>
    /// Aggregates channel and reader for <see cref="OrdersMessage"/>.
    /// </summary>
    public interface IOrdersChannel
    {
        public Channel<OrdersMessage> Channel { get; }
        public ChannelReader<OrdersMessage> Reader { get; }
    }
}