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

            CreateMap<ServiceCategory, ServiceCategoryViewModelShort>();
            CreateMap<ServiceCategory, ServiceCategoryViewModelFull>();
            CreateMap<ServiceCategoryViewModelShort, ServiceCategory>();
            
            CreateMap<Service, ServiceViewModelShort>();
            CreateMap<Service, ServiceViewModelFull>().ForMember(x => x.Category, x => x.MapFrom(z => z.Category.Name));
            CreateMap<Service, ServiceViewModelWithRelations>().ForMember(x => x.Category, x => x.MapFrom(z => z.Category.Name));

            CreateMap<Service, ServiceViewModelCreateShort>();
            CreateMap<Service, ServiceViewModelCreateFull>();
            CreateMap<ServiceViewModelCreateShort, Service>().ForMember(x => x.IsApproved, x => x.Ignore());

            CreateMap<ServiceProviderService, ServiceProviderServiceRelationViewModel>();
            CreateMap<ServiceProviderServiceRelationViewModel, ServiceProviderService>();

            CreateMap<ServiceProviderService, ServiceProviderServiceFullViewModel>();
            CreateMap<ServiceProviderService, ServiceProviderServiceViewModelCustomer>();
        }
    }    
}