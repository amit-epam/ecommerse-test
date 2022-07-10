using Microsoft.EntityFrameworkCore;

namespace ECommerse.API.Customers.Db
{
    public class CustomersDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomersDbContext(DbContextOptions options) : base(options)
        {

        }

    }
}
