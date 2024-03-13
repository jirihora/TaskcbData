using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderAgregator.Services
{
    internal class OrderAgregatorPeriodicService : BackgroundService
    {
        private readonly TimeSpan _period = TimeSpan.FromSeconds(20);

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                await base.StartAsync(cancellationToken);
                await OnStartedAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                Log.Error(ex, $"Error while starting {nameof(OrderAgregatorPeriodicService)}");
            }
        }

        protected Task OnStartedAsync(CancellationToken cancellationToken)
        {
            Log.Information($"{nameof(OrderAgregatorPeriodicService)} successfully started.");

            return Task.CompletedTask;
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
            await OnStoppedAsync(cancellationToken);
        }

        protected virtual Task OnStoppedAsync(CancellationToken cancellationToken)
        {
            Log.Information($"{nameof(OrderAgregatorPeriodicService)} successfully stopped.");

            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new PeriodicTimer(_period);
            while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
            {
                Log.Debug($"{DateTime.Now.ToLongTimeString()} from {nameof(OrderAgregatorPeriodicService)}.");
            }
        }
    }
}
