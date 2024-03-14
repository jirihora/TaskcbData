using Common.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderAgregator.Channels;
using OrderAgregator.Channels.Interfaces;
using OrderAgregator.Database;
using OrderAgregator.Repositories;
using OrderAgregator.Repositories.Interfaces;
using OrderAgregator.Services;
using OrderAgregator.Services.Interfaces;
using OrderAgregator.Settings;
using Serilog;

namespace OrderAgregatorAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog(SeriLogger.Configure);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddSingleton(GetOrderAgregatorPeriodicServiceSettings(builder));

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("OrderAgregatorInMemoryDB"))
            );

            builder.Services.AddTransient<IOrderService, OrderService>();
            builder.Services.AddSingleton<IOrdersChannel, OrdersChannel>();
            builder.Services.AddSingleton<IOrdersChannel, OrdersChannel>();
            builder.Services.AddTransient<OrdersProducer>();
            builder.Services.AddTransient<IOrdersProducer>(x => x.GetRequiredService<OrdersProducer>());
            builder.Services.AddHostedService<OrderAgregatorPeriodicService>();
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

            app.Run();
        }

        private static OrderAgregatorPeriodicServiceSettings GetOrderAgregatorPeriodicServiceSettings(WebApplicationBuilder builder)
        {
            OrderAgregatorPeriodicServiceSettings orderAgregatorPeriodicServiceSettings = new();

            builder.Configuration.GetSection(nameof(OrderAgregatorPeriodicServiceSettings)).Bind(orderAgregatorPeriodicServiceSettings);

            return orderAgregatorPeriodicServiceSettings;
        }
    }
}
