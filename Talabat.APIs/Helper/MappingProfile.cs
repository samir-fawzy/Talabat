using AutoMapper;
using Talabat.APIs.Dtos;
using TalabatProject.Core.Entity;
using TalabatProject.Core.Entity.Identity;
using TalabatProject.Core.Entity.Order_Aggregatoin;

namespace Talabat.APIs.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(PD => PD.ProductBrandName,O => O.MapFrom(P => P.ProductBrand.Name))
                .ForMember(PD => PD.ProductTypeName,O => O.MapFrom(P => P.ProductType.Name))
                .ForMember(PD => PD.PictureUrl,O => O.MapFrom<ProdcutPictureUrlResolver>());

            CreateMap<ProductBrand, BrandToReturnDto>();
            CreateMap<ProductType, ProductTypeToReturnDto>();
            CreateMap<TalabatProject.Core.Entity.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<AddressDto, TalabatProject.Core.Entity.Order_Aggregatoin.Address>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto,BasketItem>();
        }
    }
}
