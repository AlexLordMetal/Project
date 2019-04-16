using AutoMapper;
using ServicesApp.DataProvider.DataModels;
using ServicesApp.DataProvider.IdentityModels;
using ServicesApp.ViewModels.IdentityViewModels;
using ServicesApp.ViewModels.ViewModels;

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

            CreateMap<ServiceCategory, ShortServiceCategoryViewModel>();
            CreateMap<ShortServiceCategoryViewModel, ServiceCategory>();

            CreateMap<ServiceCategory, FullServiceCategoryViewModel>();
            CreateMap<FullServiceCategoryViewModel, ServiceCategory>();

            CreateMap<Service, ShortServiceViewModel>().ForMember(x => x.Category, x => x.MapFrom(z => z.Category.Name));

            CreateMap<Service, FullServiceViewModel>().ForMember(x => x.Category, x => x.MapFrom(z => z.Category.Name));

            CreateMap<Service, CreateServiceViewModel>();
            CreateMap<CreateServiceViewModel, Service>();

        }
    }    
}