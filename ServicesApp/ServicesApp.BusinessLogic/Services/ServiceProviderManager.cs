using AutoMapper;
using ServicesApp.BusinessLogic.Interfaces;
using ServicesApp.DataProvider;
using ServicesApp.DataProvider.DataModels;
using ServicesApp.DataProvider.IdentityModels;
using ServicesApp.ViewModels.IdentityViewModels;
using ServicesApp.ViewModels.ViewModels;
using System;
using System.Data.Entity.Migrations;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Services
{
    public class ServiceProviderManager : IServiceProviderManager, IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        private readonly IMapper _mapper;

        public ServiceProviderManager(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceProviderProfileViewModel> GetServiceProviderProfileAsync(string userId)
        {
            var serviceProviderProfile = await context.ServiceProviderProfiles.FindAsync(userId);
            ServiceProviderProfileViewModel serviceProviderProfileViewModel = _mapper.Map<ServiceProviderProfileViewModel>(serviceProviderProfile);
            return serviceProviderProfileViewModel;
        }

        public async Task UpdateServiceProviderProfileAsync(ServiceProviderProfileViewModel serviceProviderProfileViewModel, string userId)
        {
            ServiceProviderProfile serviceProviderProfile = _mapper.Map<ServiceProviderProfile>(serviceProviderProfileViewModel);
            serviceProviderProfile.Id = userId;
            context.ServiceProviderProfiles.AddOrUpdate(serviceProviderProfile);
            await context.SaveChangesAsync();
        }

        public async Task<ServiceProviderServiceRelationViewModel> GetServiceRelationAsync(string serviceProviderId, int serviceId)
        {
            var dataModel = await context.ServiceProviderServices.FindAsync(serviceProviderId, serviceId);
            var viewModel = _mapper.Map<ServiceProviderServiceRelationViewModel>(dataModel);
            return viewModel;
        }

        public async Task AddOrUpdateServiceRelationAsync(ServiceProviderServiceRelationViewModel viewModel)
        {
            var dataModel = _mapper.Map<ServiceProviderService>(viewModel);
            context.ServiceProviderServices.AddOrUpdate(dataModel);
            await context.SaveChangesAsync();
        }

        public async Task DeleteServiceRelationAsync(ServiceProviderServiceRelationDeleteViewModel viewModel)
        {
            var dataModel = _mapper.Map<ServiceProviderService>(viewModel);
            context.ServiceProviderServices.Remove(dataModel);
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}