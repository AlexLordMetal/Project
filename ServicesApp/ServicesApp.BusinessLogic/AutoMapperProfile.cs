using AutoMapper;
using ServicesApp.DataProvider.IdentityModels;
using ServicesApp.ViewModels.IdentityViewModels;

namespace ServicesApp.BusinessLogic
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CustomerProfile, CustomerProfileViewModel>();
            CreateMap<CustomerProfileViewModel, CustomerProfile>();

            CreateMap<ServiceProviderProfile, ServiceProviderProfileViewModel>();
            CreateMap<ServiceProviderProfileViewModel, ServiceProviderProfile>();
        }
    }    
}