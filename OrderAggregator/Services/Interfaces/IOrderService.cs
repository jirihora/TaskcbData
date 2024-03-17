using OrderAggregator.Model.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderAggregator.Services.Interfaces
{
    /// <summary>
    /// Provides methods to work with orders.
    /// </summary>
    public interface IOrderService
    {
        Task<IEnumerable<Order>> UploadOrders(Order[] orders);
    }
}
