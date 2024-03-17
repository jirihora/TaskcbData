using System.Collections.Generic;

namespace OrderAggregator.Model.Model
{
    public class OrdersMessage
    {
        public IEnumerable<Order> Orders { get; set; }
    }
}
