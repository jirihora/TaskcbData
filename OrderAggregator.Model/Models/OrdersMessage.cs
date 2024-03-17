using System.Collections.Generic;

namespace OrderAggregator.Model.Models
{
    public class OrdersMessage
    {
        public IEnumerable<Order> Orders { get; set; }
    }
}
