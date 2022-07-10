using ECommerse.API.Products.Providers;
using ECommerse.API.Products.Db;
using ECommerse.API.Products.Profiles;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace ECommerse.API.Products.Tests
{
    public class ProductsServiceTest
    {
        [Fact]
        public async Task GetProductsReturnsAllProducts ()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
                .Options;

            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var mapperConfig = new AutoMapper.MapperConfiguration( cfg => cfg.AddProfile(productProfile));

            var mapper = new Mapper(mapperConfig);
            var productsProvider = new ProductsProvider(dbContext,null,mapper);
            var products = await productsProvider.GetProductsAsync();
            Assert.True(products.IsSuccess);
            Assert.True(products.Products.Any());
            Assert.Null(products.ErrorMessage);

        }

        [Fact]
        public async Task GetProductReturnsProductUsingValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductReturnsProductUsingValidId))
                .Options;

            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var mapperConfig = new AutoMapper.MapperConfiguration(cfg => cfg.AddProfile(productProfile));

            var mapper = new Mapper(mapperConfig);
            var productsProvider = new ProductsProvider(dbContext, null, mapper);
            var products = await productsProvider.GetProductsAsync(1);
            Assert.True(products.IsSuccess);
            Assert.NotNull(products.Product);
            Assert.True(products.Product.Id == 1);
            Assert.Null(products.ErrorMessage);

        }

        [Fact]
        public async Task GetProductReturnsProductUsingInValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductReturnsProductUsingValidId))
                .Options;

            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var mapperConfig = new AutoMapper.MapperConfiguration(cfg => cfg.AddProfile(productProfile));

            var mapper = new Mapper(mapperConfig);
            var productsProvider = new ProductsProvider(dbContext, null, mapper);
            var products = await productsProvider.GetProductsAsync(-1);
            Assert.False(products.IsSuccess);
            Assert.Null(products.Product);
            //Assert.True(products.Product.Id == 1);
            Assert.NotNull(products.ErrorMessage);

        }

        private void CreateProducts(ProductsDbContext dbContext)
        {
            for(int i=1;i<=10;i++)
            {
                dbContext.Products.Add(
                    new Product()
                    {
                        Id = i,
                        Name = Guid.NewGuid().ToString(),
                        Inventory = i + 10,
                        Price = i * 3
                    });

            }

            dbContext.SaveChanges();
        }

    }
}