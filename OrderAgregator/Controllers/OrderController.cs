using Microsoft.AspNetCore.Mvc;
using OrderAggregator.Model.Model;
using OrderAggregator.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderAggregator.Controllers
{
    /// <summary>
    /// Controller for working with orders.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderAggregatorService;

        public OrderController(IOrderService orderAggregatorService)
        {
            _orderAggregatorService = orderAggregatorService;
        }

        [HttpPost]
        public async Task<IEnumerable<Order>> UploadOrders([FromBody] Order[] orders)
        {
            return await _orderAggregatorService.UploadOrders(orders);
        }
    }
}
