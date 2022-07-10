using AutoMapper;
using ECommerse.API.Orders.Db;
using ECommerse.API.Orders.Interfaces;
using ECommerse.API.Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerse.API.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext dbContext;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;

        public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> logger
            , IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (dbContext.Orders.Any() == false)
            {
                dbContext.Orders.Add(new Order() { Id = 1, OrderName= "ordKeyboard" , CustomerId = 1, OrderDate = DateTime.Now.AddDays(-1), Total = 100});
                dbContext.Orders.Add(new Order() { Id = 2, OrderName= "ordMouse", CustomerId = 1, OrderDate = DateTime.Now.AddDays(-2), Total = 200});
                dbContext.Orders.Add(new Order() { Id = 3, OrderName= "ordMonitor", CustomerId = 2, OrderDate = DateTime.Now.AddDays(-3), Total = 300});
                dbContext.Orders.Add(new Order() { Id = 4, OrderName= "ordCPU", CustomerId = 3, OrderDate = DateTime.Now.AddDays(-4), Total = 400 });

                dbContext.OrderItems.Add(new OrderItem() { Id = 1, OrderId=1, ProductId = 1, Quantity = 1, UnitPrice = 100 });
                dbContext.OrderItems.Add(new OrderItem() { Id = 2, OrderId = 2, ProductId = 2, Quantity = 2, UnitPrice = 100 });
                dbContext.OrderItems.Add(new OrderItem() { Id = 3, OrderId = 3, ProductId = 3, Quantity = 3, UnitPrice = 100 });
                dbContext.OrderItems.Add(new OrderItem() { Id = 4, OrderId = 4, ProductId = 4, Quantity = 4, UnitPrice = 100 });

                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<OrderModel> Orders, string ErrorMessage)> GetOrdersAsync()
        {
            try
            {
                var Orders = await dbContext.Orders.ToListAsync();

                if (Orders != null && Orders.Any())
                {
                    var result = mapper.Map<IEnumerable<Order>, IEnumerable<OrderModel>>(Orders);
                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, OrderModel Order, string ErrorMessage)> GetOrdersAsync(int id)
        {
            try
            {
                var Order = await dbContext.Orders.FirstAsync(p => p.Id == id);

                if (Order != null)
                {
                    var result = mapper.Map<Order, OrderModel>(Order);
                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<OrderModel> Orders, string ErrorMessage)> GetOrdersByCustomerIdAsync(int customerId)
        {
            try
            {
                var Order = await dbContext.Orders.Where(p => p.CustomerId == customerId).ToListAsync();

                if (Order != null)
                {
                    var result = mapper.Map<IEnumerable<Order>, IEnumerable< OrderModel> >(Order);

                    foreach (var ord in result)
                    {
                        var orderItems = await dbContext.OrderItems.Where(o => o.OrderId == ord.Id).ToListAsync();
                        ord.OderItems = mapper.Map<List<OrderItem>, List<OrderItemModel>>(orderItems);
                    }

                    return (true, result, null);
                }

                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
