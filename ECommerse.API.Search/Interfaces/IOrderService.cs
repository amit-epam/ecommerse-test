using ECommerse.API.Search.Models;

namespace ECommerse.API.Search.Interfaces
{
    public interface IOrdersService
    {
        Task<(bool IsSuccess,IEnumerable<OrderModel> Orders,string ErrorMessage)> GetOrdersAsync(int customerId);
    }
}
