using AutoMapper;
using GMarket.Domain.Entities.IdentityCustomer;
using GMarket.Domain.Entities.Market;
using GMarket.Domain.ViewModels;

namespace GMarket.Service;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<UserSecurity, LoginViewModel>().ReverseMap();
        CreateMap<UserSecurity, RegisterViewModel>().ReverseMap();
        CreateMap<ConfirmModel, RegisterViewModel>().ReverseMap();
        CreateMap<GoogleOAuth, RegisterViewModel>().ReverseMap();
        
        CreateMap<ProductOptionsViewModel, ProductItem>().ReverseMap();
        CreateMap<ProductOptionsViewModel, Product>().ReverseMap();
    }
}