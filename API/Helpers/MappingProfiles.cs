using API.Dtos;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Creating a map between Product and ProductToReturnDto classes (DTOs) using CreateMap method
            CreateMap<Product, ProductToReturnDto>()

            // Configuring the properties of DTOs to be mapped from Product entity's properties.
            .ForMember(dto => dto.ProductBrand, product => product.MapFrom(sourceMember => sourceMember.ProductBrand.Name))
            .ForMember(dto => dto.ProductType, product => product.MapFrom(sourceMember => sourceMember.ProductType.Name))
            .ForMember(dto => dto.PictureUrl, product => product.MapFrom<ProductUrlResolver>());
        }
    }
}