using ECommerse.API.Search.Interfaces;

namespace ECommerse.API.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;
        public SearchService(IOrdersService ordersService, IProductsService productsService)
        {
            this.ordersService = ordersService;
            this.productsService = productsService;
        }

        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var orderResult = await ordersService.GetOrdersAsync(customerId);
            var productsResult = await productsService.GetProductsAsync();

            if (orderResult.IsSuccess )
            {                

                foreach(var order in orderResult.Orders)
                {
                    foreach(var item in order.OderItems)
                    {
                        item.ProductName = productsResult.IsSuccess ? 
                            productsResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name
                            : "Product information is not available";
                    }
                }

                var result = new {
                    Orders = orderResult.Orders
                };

                return (true, result);
            }

            return (false, null);
        }


    }
}
