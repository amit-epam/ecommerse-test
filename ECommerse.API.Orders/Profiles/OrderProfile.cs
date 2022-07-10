using ECommerse.API.Orders.Db;
using ECommerse.API.Orders.Models;

namespace ECommerse.API.Orders.Profiles
{
    public class OrderProfile : AutoMapper.Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderModel>();
            CreateMap<OrderItem, OrderItemModel>();
        }
    }
}
