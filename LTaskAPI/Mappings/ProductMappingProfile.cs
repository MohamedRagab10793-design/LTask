using AutoMapper;
using LTaskAPI.Data.Entities;
using LTaskAPI.DTOs;

namespace LTaskAPI.Mappings;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {   
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
        CreateMap<Product, ProductDto>();
    }
}
