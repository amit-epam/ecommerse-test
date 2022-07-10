using AutoMapper;
using ECommerse.API.Customers.Db;
using ECommerse.API.Customers.Interfaces;
using ECommerse.API.Customers.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerse.API.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext dbContext;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;

        public CustomersProvider(CustomersDbContext dbContext, ILogger<CustomersProvider> logger
            , IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (dbContext.Customers.Any() == false)
            {
                dbContext.Customers.Add(new Customer() { Id = 1, CustomerName = "Cust Keyboard" , Address = "Addr 1"});
                dbContext.Customers.Add(new Customer() { Id = 2, CustomerName = "Cust Mouse" , Address = "Addr 2"});
                dbContext.Customers.Add(new Customer() { Id = 3, CustomerName = "Cust Monitor" , Address = "Addr 3"});
                dbContext.Customers.Add(new Customer() { Id = 4, CustomerName = "Cust CPU" , Address = "Addr 4"});
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<CustomerModel> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var Customers = await dbContext.Customers.ToListAsync();

                if (Customers != null && Customers.Any())
                {
                    var result = mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerModel>>(Customers);
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

        public async Task<(bool IsSuccess, CustomerModel Customer, string ErrorMessage)> GetCustomersAsync(int id)
        {
            try
            {
                var Customer = await dbContext.Customers.FirstAsync(p => p.Id == id);

                if (Customer != null)
                {
                    var result = mapper.Map<Customer, CustomerModel>(Customer);
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
