using Microsoft.Extensions.Hosting;
using OrderAgregator.Channels.Interfaces;
using OrderAgregator.Helpers;
using OrderAgregator.Model.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderAgregator.Services
{
    internal class OrderAgregatorPeriodicService : BackgroundService
    {
        private readonly TimeSpan _period = TimeSpan.FromSeconds(20);
        private readonly IOrdersChannel _ordersChannel;
        private List<Order> _orders = [];

        public OrderAgregatorPeriodicService(IOrdersChannel ordersChannel)
        {
            _ordersChannel = ordersChannel;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                await base.StartAsync(cancellationToken);
                await OnStartedAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while starting {ServiceName}", nameof(OrderAgregatorPeriodicService));
            }
        }

        protected Task OnStartedAsync(CancellationToken cancellationToken)
        {
            Log.Information("{ServiceName} successfully started.", nameof(OrderAgregatorPeriodicService));

            return Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
            await OnStoppedAsync(cancellationToken);
        }

        protected virtual Task OnStoppedAsync(CancellationToken cancellationToken)
        {
            Log.Information("{ServiceName} successfully stopped.", nameof(OrderAgregatorPeriodicService));

            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await ProcessOrders(cancellationToken);
        }

        private async Task ProcessOrders(CancellationToken cancellationToken)
        {
            Log.Debug("{DateTime} from {ServiceName}.", DateTime.Now.ToLongTimeString(), nameof(OrderAgregatorPeriodicService));

            try
            {
                using PeriodicTimer timer = new PeriodicTimer(_period);
                //wait for timer tick
                while (!cancellationToken.IsCancellationRequested && await timer.WaitForNextTickAsync(cancellationToken))
                {
                    // read all messages from channel
                    while (_ordersChannel.Reader.TryRead(out OrdersMessage ordersMessage))
                    {
                        _orders.AddRange(ordersMessage.Orders);
                    }

                    // agregate orders
                    var agregatedOrders = OrderAgregationHelper.AggregateOrders(_orders);
                    LogOrders(OrderAgregationHelper.TransformToList(agregatedOrders));

                    // clear temp orders
                    _orders.Clear();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in {ServiceName}", nameof(OrderAgregatorPeriodicService));

                throw;
            }
        }

        private static void LogOrders(IEnumerable<Order> orders)
        {
            Log.Information("{Count} orders:", orders.Count());

            foreach (Order order in orders)
            {
                Log.Information($"{nameof(OrderAgregatorPeriodicService)}: Order: product id: {order.ProductId}, quantity: {order.Quantity}.");
            }
        }
    }
}
