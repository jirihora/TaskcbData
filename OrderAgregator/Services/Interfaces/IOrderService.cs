using OrderAgregator.Model.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderAgregator.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> UploadOrders(Order[] orders);
    }
}
