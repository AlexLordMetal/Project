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
            
            CreateMap<Service, ServiceViewModel>().ForMember(x => x.Category, x => x.MapFrom(z => z.Category.Name));
            CreateMap<Service, ServiceViewModelWithRelations>().ForMember(x => x.Category, x => x.MapFrom(z => z.Category.Name));

            CreateMap<Service, ServiceViewModelCreate>();
            CreateMap<ServiceViewModelCreate, Service>().ForMember(x => x.IsApproved, x => x.Ignore()).ForMember(x => x.Photo, x => x.Ignore());

            CreateMap<ServiceProviderService, ProviderServiceRelationViewModel>();
            CreateMap<ProviderServiceRelationViewModel, ServiceProviderService>().ForMember(x => x.Photo, x => x.Ignore());

            CreateMap<ServiceProviderService, ProviderServiceFullViewModel>();
            CreateMap<ServiceProviderService, ProviderServiceViewModelCustomer>();

            CreateMap<OrderViewModelCustomer, Order>();
            CreateMap<OrderViewModelShort, Order>();
            CreateMap<Order, OrderViewModelCustomer>();
            CreateMap<Order, OrderViewModelServiceProvider>();

            CreateMap<PhotoViewModel, Photo>();
            CreateMap<Photo, PhotoViewModel>();
        }
    }    
}