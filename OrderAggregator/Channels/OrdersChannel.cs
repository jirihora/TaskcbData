using OrderAggregator.Channels.Interfaces;
using OrderAggregator.Model.Models;
using System.Threading.Channels;

namespace OrderAggregator.Channels
{
    /// <summary>
    /// Aggregates channel and reader for <see cref="OrdersMessage"/> using <see cref="System.Threading.Channels.Channel"/>.
    /// </summary>
    public class OrdersChannel : IOrdersChannel
    {
        public OrdersChannel()
        {
            Channel = System.Threading.Channels.Channel.CreateUnbounded<OrdersMessage>();
            Reader = Channel.Reader;
        }

        public Channel<OrdersMessage> Channel { get; }

        public ChannelReader<OrdersMessage> Reader { get; }
    }
}
