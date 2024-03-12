using Microsoft.AspNetCore.Mvc;
using OrderAgregator.Model.Model;
using System.Threading.Tasks;

namespace OrderAgregatorAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> UploadOrders(Order[] orders)
        {



            return new AcceptedResult();
        }
    }
}
