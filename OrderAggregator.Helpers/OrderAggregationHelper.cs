using OrderAggregator.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace OrderAggregator.Helpers
{
    /// <summary>
    /// Helper class for working with orders.
    /// </summary>
    public static class OrderAggregationHelper
    {
        /// <summary>
        /// Aggregates orders by product id and sums the quantity.
        /// </summary>
        /// <param name="orders">List of orders.</param>
        /// <returns>Dictionary where key is product id and value is quantity.</returns>
        public static Dictionary<string, int> AggregateOrders(IEnumerable<Order> orders)
        {
            Dictionary<string, int> agregatedOrders = [];

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

        /// <summary>
        /// Transforms aggregated dictionary to list of orders.
        /// </summary>
        /// <param name="aggregatedOrders">Dictionary with aggregated orders.</param>
        /// <returns>List of orders.</returns>
        public static List<Order> TransformToList(Dictionary<string, int> aggregatedOrders)
        {
            return aggregatedOrders
                    .Select(x => new Order { ProductId = x.Key, Quantity = x.Value })
                    .ToList();
        }
    }
}
