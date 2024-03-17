using FluentAssertions;
using OrderAggregator.Model.Models;
using System.Collections.Generic;

namespace OrderAggregator.Helpers.Tests
{
    [TestClass]
    public class OrderAggregationHelperTests
    {
        [TestMethod]
        public void AggregateOrders_ReturnsAggregatedOrdersWithRightQuantities()
        {
            var order = new List<Order>
            {
                new() { ProductId = "1", Quantity = 1 },
                new() { ProductId = "10", Quantity = 10 },
                new() { ProductId = "100", Quantity = 100 },
                new() { ProductId = "1", Quantity = 1 },
                new() { ProductId = "10", Quantity = 10 },
                new() { ProductId = "100", Quantity = 100 },
                new() { ProductId = "1", Quantity = 1 },
                new() { ProductId = "10", Quantity = 10 },
                new() { ProductId = "100", Quantity = 100 }
            };

            Dictionary<string, int> aggregatedOrders = OrderAggregationHelper.AggregateOrders(order);

            aggregatedOrders.Should().NotBeNullOrEmpty();
            aggregatedOrders.Should().HaveCount(3);
            aggregatedOrders.Should().Contain(x => x.Key == "1" && x.Value == 3);
            aggregatedOrders.Should().Contain(x => x.Key == "10" && x.Value == 30);
            aggregatedOrders.Should().Contain(x => x.Key == "100" && x.Value == 300);
        }

        [TestMethod]
        public void TransformToList()
        {
            var aggregatedOrders = new Dictionary<string, int>()
            {
                    { "1", 3 },
                    { "10", 30 },
                    { "100", 300 }
            };

            var orders = OrderAggregationHelper.TransformToList(aggregatedOrders);

            orders.Should().NotBeNullOrEmpty();
            orders.Should().HaveCount(3);
            orders.Should().Contain(x => x.ProductId == "1" && x.Quantity == 3);
            orders.Should().Contain(x => x.ProductId == "10" && x.Quantity == 30);
            orders.Should().Contain(x => x.ProductId == "100" && x.Quantity == 300);
        }
    }
}
