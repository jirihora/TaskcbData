using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using OrderAggregator.Database;
using OrderAggregator.Model.Model;
using OrderAggregator.Repositories;
using System;
using System.Collections.Generic;

namespace OrderAggregator.Tests
{
    [TestClass]
    public class OrderRepositoryTests
    {
        private readonly AppDbContext _appDbContext;
        private readonly OrderRepository _orderRepository;

        public OrderRepositoryTests()
        {
            DbContextOptionsBuilder<AppDbContext> dbOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(
                    Guid.NewGuid().ToString()
                );

            _appDbContext = new AppDbContext(dbOptions.Options);

            _orderRepository = new OrderRepository(_appDbContext);
        }

        [TestMethod]
        public async Task SaveOrdersAsync_ReturnsCountOfSavedOrders()
        {
            var orders = new List<Order>
            {
                new() { ProductId = "1", Quantity = 1 },
                new() { ProductId = "10", Quantity = 10 },
                new() { ProductId = "100", Quantity = 100 }
            };

            var result = await _orderRepository.SaveOrdersAsync(orders);

            result.Should().Be(3);
        }

        public async Task GetAllOrdersAsync_ReturnsSavedOrders()
        {
            var orders = new List<Order>
            {
                new() { Id = 1, ProductId = "1", Quantity = 1 },
                new() { Id = 2, ProductId = "10", Quantity = 10 },
                new() { Id = 3, ProductId = "100", Quantity = 100 }
            };

            _appDbContext.Orders.AddRange(orders);
            await _appDbContext.SaveChangesAsync();

            var result = await _orderRepository.GetAllOrdersAsync();

            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(3);
            result.Should().Contain(orders);
        }
    }
}
