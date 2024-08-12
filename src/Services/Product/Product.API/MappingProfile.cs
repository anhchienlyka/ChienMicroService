using AutoMapper;
using Product.API.Entities;
using Shared.DTOs.Product;

namespace Product.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CatalogProduct, ProductDto>().ReverseMap();
            CreateMap<UpdateProductDto, CatalogProduct>().ReverseMap();
            CreateMap<CreateProductDto, CatalogProduct>().ReverseMap();
        }
    }
}