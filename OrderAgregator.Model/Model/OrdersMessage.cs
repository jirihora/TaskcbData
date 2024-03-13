using System.Collections.Generic;

namespace OrderAgregator.Model.Model
{
    public class OrdersMessage
    {
        public IEnumerable<Order> Orders { get; set; }
    }
}
