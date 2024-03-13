using OrderAgregator.Channels.Interfaces;
using OrderAgregator.Model.Model;
using System.Threading.Channels;

namespace OrderAgregator.Channels
{
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
