using Microsoft.AspNetCore.Mvc;
using OrderAgregator.Model.Model;
using OrderAgregator.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderAgregatorAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderAgregatorService;

        public OrderController(IOrderService orderAgregatorService)
        {
            _orderAgregatorService = orderAgregatorService;
        }

        [HttpPost]
        public async Task<IEnumerable<Order>> UploadOrders([FromBody] Order[] orders)
        {
            return await _orderAgregatorService.UploadOrders(orders);
        }
    }
}
