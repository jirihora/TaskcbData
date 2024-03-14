using Microsoft.AspNetCore.Mvc;
using OrderAgregator.Model.Model;
using OrderAgregator.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderAgregator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestingController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public TestingController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Order>> GetAllSavedOrders()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }
    }
}
