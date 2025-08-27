using AutoMapper;
using ProductsCRUD.DTOs.Product;
using ProductsCRUD.Models;

namespace ProductsCRUD.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
        }
    }
}