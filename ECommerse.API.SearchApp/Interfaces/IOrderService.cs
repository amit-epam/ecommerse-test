using ECommerse.API.SearchApp.Models;

namespace ECommerse.API.SearchApp.Interfaces
{
    public interface IOrdersService
    {
        Task<(bool IsSuccess,IEnumerable<OrderModel>,string ErrorMessage)> GetOrdersAsync(int customerId);
    }
}
