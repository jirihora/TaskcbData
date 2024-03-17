using OrderAggregator.Channels.Interfaces;
using OrderAggregator.Model.Model;
using Serilog;
using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace OrderAggregator.Channels
{
    /// <summary>
    /// Bundles methods that are used to produce <see cref="OrdersMessage"/> using <see cref="IOrdersChannel"/>
    /// </summary>
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
