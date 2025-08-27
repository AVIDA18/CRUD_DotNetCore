using AutoMapper;
using ProductsCRUD.DTOs.ProductCategory;
using ProductsCRUD.Models;

namespace ProductsCRUD.Mappings
{
    public class ProductCategoryProfile : Profile
    {
        public ProductCategoryProfile()
        {
            //Domain-->Dto
            CreateMap<ProductCategory, ProductCategoryDto>();
            //Dto-->Domain
            CreateMap<ProductCategoryDto, ProductCategory>();
        }
    }
}