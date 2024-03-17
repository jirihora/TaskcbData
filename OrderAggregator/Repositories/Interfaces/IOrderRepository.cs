using OrderAggregator.Model.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderAggregator.Repositories.Interfaces
{
    /// <summary>
    /// Bundles all the methods that are used to process orders.
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Method to save orders.
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
