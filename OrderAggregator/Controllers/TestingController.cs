#if DEBUG

using Microsoft.AspNetCore.Mvc;
using OrderAggregator.Model.Models;
using OrderAggregator.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderAggregator.Controllers
{
    /// <summary>
    /// Controller for testing purposes.
    /// </summary>
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

#endif
