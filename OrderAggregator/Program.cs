using Common.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderAggregator.Channels;
using OrderAggregator.Channels.Interfaces;
using OrderAggregator.Database;
using OrderAggregator.Repositories;
using OrderAggregator.Repositories.Interfaces;
using OrderAggregator.Services;
using OrderAggregator.Services.Interfaces;
using OrderAggregator.Settings;
using Serilog;
using System;

namespace OrderAggregator
{
    public class Program
    {
        private const string OrderAggregatorInMemoryDB = nameof(OrderAggregatorInMemoryDB);

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog(SeriLoggerExtensions.Configure);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHealthChecks();

            builder.Services.AddSingleton(GetOrderAggregatorPeriodicServiceSettings(builder));

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase(GetOrderAggregatorInMemoryDBConnectionString(builder))
            );

            builder.Services.AddTransient<IOrderService, OrderService>();
            builder.Services.AddSingleton<IOrdersChannel, OrdersChannel>();
            builder.Services.AddSingleton<IOrdersChannel, OrdersChannel>();
            builder.Services.AddTransient<OrdersProducer>();
            builder.Services.AddTransient<IOrdersProducer>(x => x.GetRequiredService<OrdersProducer>());
            builder.Services.AddHostedService<OrderAggregatorPeriodicService>();
            builder.Services.AddTransient<IOrderRepository, OrderRepository>();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.MapHealthChecks("/api/health");

            app.Run();
        }

        private static string GetOrderAggregatorInMemoryDBConnectionString(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString(OrderAggregatorInMemoryDB);

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException($"Cannot get connection string for {OrderAggregatorInMemoryDB}.");
            }

            return connectionString;
        }

        private static OrderAggregatorPeriodicServiceSettings GetOrderAggregatorPeriodicServiceSettings(WebApplicationBuilder builder)
        {
            OrderAggregatorPeriodicServiceSettings orderAggregatorPeriodicServiceSettings = new();

            builder.Configuration.GetSection(nameof(OrderAggregatorPeriodicServiceSettings)).Bind(orderAggregatorPeriodicServiceSettings);

            if (orderAggregatorPeriodicServiceSettings.IsDefault())
            {
                throw new ArgumentException($"{nameof(OrderAggregatorPeriodicServiceSettings)} is set to default. The application won't start.");
            }

            return orderAggregatorPeriodicServiceSettings;
        }
    }
}
