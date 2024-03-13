using OrderAgregator.Model.Model;
using System.Collections.Generic;
using System.Linq;

namespace OrderAgregator.Helpers
{
    public static class OrderAgregationHelper
    {
        public static Dictionary<string, int> AggregateOrders(IEnumerable<Order> orders)
        {
            Dictionary<string, int> agregatedOrders = new();

            foreach (var order in orders)
            {
                if (agregatedOrders.ContainsKey(order.ProductId))
                {
                    agregatedOrders[order.ProductId] += order.Quantity;
                }
                else
                {
                    agregatedOrders.Add(order.ProductId, order.Quantity);
                }
            }

            return agregatedOrders;
        }

        public static List<Order> TransformToList(Dictionary<string, int> agregatedOrders)
        {
            return agregatedOrders.Select(kvp => new Order { ProductId = kvp.Key, Quantity = kvp.Value })
                      .ToList();
        }
    }
}
