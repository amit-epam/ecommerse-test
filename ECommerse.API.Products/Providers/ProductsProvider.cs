using AutoMapper;
using ECommerse.API.Products.Db;
using ECommerse.API.Products.Interfaces;
using ECommerse.API.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerse.API.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {

        private readonly ProductsDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider> logger
            , IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if(dbContext.Products.Any() == false)
            {
                dbContext.Products.Add(new Product(){ Id = 1 , Name = "Keyboard" , Inventory = 5 , Price = 100 });
                dbContext.Products.Add(new Product() { Id = 2, Name = "Mouse", Inventory = 15, Price = 200 });
                dbContext.Products.Add(new Product() { Id = 3, Name = "Monitor", Inventory = 25, Price = 300 });
                dbContext.Products.Add(new Product() { Id = 4, Name = "CPU", Inventory = 35, Price = 400 });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<ProductModel> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();

                if(products != null && products.Any())
                {
                   var result =  mapper.Map<IEnumerable<Product>, IEnumerable<ProductModel>>(products);
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

        public async Task<(bool IsSuccess, ProductModel Product, string ErrorMessage)> GetProductsAsync(int id)
        {
            try
            {
                var product = await dbContext.Products.FirstAsync( p=> p.Id == id);

                if (product != null )
                {
                    var result = mapper.Map<Product, ProductModel>(product);
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
