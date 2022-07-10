using ECommerse.API.Customers.Db;
using ECommerse.API.Customers.Models;

namespace ECommerse.API.Customers.Profiles
{
    public class CustomerProfile : AutoMapper.Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerModel>();
        }
    }
}
