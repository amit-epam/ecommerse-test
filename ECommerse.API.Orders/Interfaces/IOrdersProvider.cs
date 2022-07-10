using ECommerse.API.Orders.Models;

namespace ECommerse.API.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool IsSuccess, IEnumerable<OrderModel> Orders, string ErrorMessage)> GetOrdersAsync();
        Task<(bool IsSuccess, OrderModel Order, string ErrorMessage)> GetOrdersAsync(int id);

        Task<(bool IsSuccess, IEnumerable<OrderModel> Orders, string ErrorMessage)> GetOrdersByCustomerIdAsync(int customerId);


    }
}
