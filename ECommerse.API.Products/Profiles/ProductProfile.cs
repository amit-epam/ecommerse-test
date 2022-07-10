using ECommerse.API.Products.Db;
using ECommerse.API.Products.Models;

namespace ECommerse.API.Products.Profiles
{
    public class ProductProfile : AutoMapper.Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductModel>();
        }
    }
}
