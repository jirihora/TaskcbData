using Microsoft.AspNetCore.Mvc;
using OrderAggregator.Model.Models;
using OrderAggregator.Model.Validations;
using OrderAggregator.Services.Interfaces;
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
        public async Task<IActionResult> UploadOrders([FromBody] Order[] orders)
        {
            foreach (var order in orders)
            {
                var validator = new OrderValidator();
                var validationResult = await validator.ValidateAsync(order);

                if (!validationResult.IsValid)
                {
                    return ValidationProblem($"Order with product id '{order.ProductId}' and quantity '{order.Quantity}' is not valid. Validation result: {validationResult}");
                }
            }

            await _orderAggregatorService.UploadOrders(orders);

            return Accepted();
        }
    }
}
