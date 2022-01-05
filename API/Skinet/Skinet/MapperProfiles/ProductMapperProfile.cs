using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Skinet.API.Dtos;
using Skinet.API.MapperResolver;
using Skinet.Core.Entities;

namespace Skinet.API.MapperProfiles
{
    public class ProductMapperProfile:Profile
    {
        public ProductMapperProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(des => des.ProductBrand,
                    x => x.MapFrom(src => src.ProductBrand.Name))
                .ForMember(des => des.ProductType, x => x.MapFrom(
                    src => src.ProductType.Name))
                .ForMember(des => des.PictureUrl, x => x.MapFrom<ProductMapperResolver>());
            CreateMap<ProductBrand, ProductBrandToReturnDto>();
            CreateMap<ProductType, ProductTypeToReturnDto>();
            CreateMap<UserDto, UserEntity>();
            CreateMap<UserEntity, UserToRetrun>();
        }
    }
}
