using FluentAssertions;
using OrderAgregator.Helpers;
using OrderAgregator.Model.Model;

namespace OrderAgregation.Helpers.Tests
{
    [TestClass]
    public class OrderAgregationHelperTests
    {
        [TestMethod]
        public void AgregateOrders_ReturnsAgregatedOrdersWithRightQuantities()
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

            Dictionary<string, int> agregatedOrders = OrderAgregationHelper.AggregateOrders(order);

            agregatedOrders.Should().NotBeNullOrEmpty();
            agregatedOrders.Should().HaveCount(3);
            agregatedOrders.Should().Contain(x => x.Key == "1" && x.Value == 3);
            agregatedOrders.Should().Contain(x => x.Key == "10" && x.Value == 30);
            agregatedOrders.Should().Contain(x => x.Key == "100" && x.Value == 300);
        }

        [TestMethod]
        public void TransformToList()
        {
            var agregatedOrders = new Dictionary<string, int>()
            {
                    { "1", 3 },
                    { "10", 30 },
                    { "100", 300 }
            };

            var orders = OrderAgregationHelper.TransformToList(agregatedOrders);

            orders.Should().NotBeNullOrEmpty();
            orders.Should().HaveCount(3);
            orders.Should().Contain(x => x.ProductId == "1" && x.Quantity == 3);
            orders.Should().Contain(x => x.ProductId == "10" && x.Quantity == 30);
            orders.Should().Contain(x => x.ProductId == "100" && x.Quantity == 300);
        }
    }
}
