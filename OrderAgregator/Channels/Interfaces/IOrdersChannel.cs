using OrderAgregator.Model.Model;
using System.Threading.Channels;

namespace OrderAgregator.Channels.Interfaces
{
    public interface IOrdersChannel
    {
        public Channel<OrdersMessage> Channel { get; }
        public ChannelReader<OrdersMessage> Reader { get; }
    }
}