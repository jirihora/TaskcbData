using Microsoft.AspNetCore.Mvc;
using OrderAgregator.Model.Model;
using System;
using System.Threading.Tasks;

namespace OrderAgregatorAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> UploadOrders([FromBody] Order[] orders)
        {
            foreach (Order order in orders)
            {
                Console.WriteLine(OrderToString(order));
            }

            return new AcceptedResult();
        }

        private string OrderToString(Order order)
        {
            return $"Order: product id: {order.ProductId}, quantity: {order.Quantity}.";
        }
    }
}