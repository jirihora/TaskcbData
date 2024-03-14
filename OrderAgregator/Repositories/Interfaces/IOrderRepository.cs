using OrderAgregator.Model.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderAgregator.Repositories.Interfaces
{
    /// <summary>
    /// Repository for working with orders.
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Methods to save orders.
        /// </summary>
        /// <param name="orders">Collection of orders to be saved.</param>
        /// <returns>Count of saved orders.</returns>
        Task<int> SaveOrdersAsync(IEnumerable<Order> orders);

        /// <summary>
        /// Returns all saved orders.
        /// </summary>
        /// <returns>List of orders.</returns>
        Task<IEnumerable<Order>> GetAllOrdersAsync();
    }
}
