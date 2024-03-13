using OrderAgregator.Channels.Interfaces;
using OrderAgregator.Model.Model;
using Serilog;
using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace OrderAgregator.Channels
{
    public class OrdersProducer : IOrdersProducer
    {
        private readonly ChannelWriter<OrdersMessage> _channelWriter;
        private readonly IOrdersChannel _ordersChannel;

        public OrdersProducer(IOrdersChannel ordersChannel)
        {
            _ordersChannel = ordersChannel;
            _channelWriter = _ordersChannel.Channel.Writer;
        }

        public async Task SendOrdersMessageAsync(OrdersMessage ordersMessage)
        {
            try
            {
                await _channelWriter.WriteAsync(ordersMessage);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to write to orders channel.");

                throw;
            }
        }
    }
}
