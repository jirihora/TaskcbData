using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderAggregator.Channels.Interfaces;
using OrderAggregator.Helpers;
using OrderAggregator.Model.Models;
using OrderAggregator.Repositories.Interfaces;
using OrderAggregator.Settings;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace OrderAggregator.Services
{
    /// <summary>
    /// Process received orders in set interval.
    /// </summary>
    internal class OrderAggregatorPeriodicService : BackgroundService
    {
        private readonly OrderAggregatorPeriodicServiceSettings _orderAggregatorPeriodicServiceSettings;
        private readonly IOrdersChannel _ordersChannel;
        private readonly IServiceProvider _serviceProvider;
        private readonly List<Order> _orders = [];

        public OrderAggregatorPeriodicService(OrderAggregatorPeriodicServiceSettings orderAggregatorPeriodicServiceSettings, IOrdersChannel ordersChannel, IServiceProvider serviceProvider)
        {
            _orderAggregatorPeriodicServiceSettings = orderAggregatorPeriodicServiceSettings;
            _ordersChannel = ordersChannel;
            _serviceProvider = serviceProvider;
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
                Log.Error(ex, "Error while starting {ServiceName}", nameof(OrderAggregatorPeriodicService));
            }
        }

        protected Task OnStartedAsync(CancellationToken cancellationToken)
        {
            Log.Information("{ServiceName} successfully started.", nameof(OrderAggregatorPeriodicService));

            return Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
            await OnStoppedAsync(cancellationToken);
        }

        protected virtual Task OnStoppedAsync(CancellationToken cancellationToken)
        {
            Log.Information("{ServiceName} successfully stopped.", nameof(OrderAggregatorPeriodicService));

            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await ProcessOrdersWithTimer(cancellationToken);
        }

        private async Task ProcessOrdersWithTimer(CancellationToken cancellationToken)
        {
            Log.Debug("{DateTime} from {ServiceName}.", DateTime.Now.ToLongTimeString(), nameof(OrderAggregatorPeriodicService));

            try
            {
                using PeriodicTimer timer = new(TimeSpan.FromSeconds(_orderAggregatorPeriodicServiceSettings.OrderAggregationInterval));
                //wait for timer tick
                while (!cancellationToken.IsCancellationRequested && await timer.WaitForNextTickAsync(cancellationToken))
                {
                    // read all messages from channel
                    while (_ordersChannel.Reader.TryRead(out OrdersMessage ordersMessage))
                    {
                        _orders.AddRange(ordersMessage.Orders);
                    }

                    // aggregate orders
                    var aggregatedOrders = OrderAggregationHelper.AggregateOrders(_orders);

                    using var scope = _serviceProvider.CreateScope();
                    var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();

                    var savedOrdersCount = await orderRepository.SaveOrdersAsync(OrderAggregationHelper.TransformToList(aggregatedOrders));

                    Log.Information("{SavedOrdersCount} orders saved.", savedOrdersCount.ToString());

                    // clear temp orders
                    _orders.Clear();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error in {ServiceName}.", nameof(OrderAggregatorPeriodicService));

                throw;
            }
        }

        private static void LogOrders(IEnumerable<Order> orders)
        {
            Log.Information("{Count} orders:", orders.Count());

            foreach (Order order in orders)
            {
                Log.Information($"{nameof(OrderAggregatorPeriodicService)}: Order: product id: {order.ProductId}, quantity: {order.Quantity}.");
            }
        }
    }
}
