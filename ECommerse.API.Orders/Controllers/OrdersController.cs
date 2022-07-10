using ECommerse.API.Orders.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerse.API.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider ordersProvider;

        public OrdersController(IOrdersProvider ordersProvider)
        {
            this.ordersProvider = ordersProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersAsync()
        {
            var result = await ordersProvider.GetOrdersAsync();

            if (result.IsSuccess)
            {
                return Ok(result.Orders);
            }

            return NotFound();
        }

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetOrdersAsync(int id)
        //{
        //    var result = await ordersProvider.GetOrdersAsync(id);

        //    if (result.IsSuccess)
        //    {
        //        return Ok(result.Order);
        //    }

        //    return NotFound();
        //}

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrdersByCustomerIdAsync(int customerId)
        {
            var result = await ordersProvider.GetOrdersByCustomerIdAsync(customerId);

            if (result.IsSuccess)
            {
                return Ok(result.Orders);
            }

            return NotFound();
        }

    }
}
