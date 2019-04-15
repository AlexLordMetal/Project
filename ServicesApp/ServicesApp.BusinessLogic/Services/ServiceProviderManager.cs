using AutoMapper;
using ServicesApp.BusinessLogic.Interfaces;
using ServicesApp.DataProvider;
using ServicesApp.DataProvider.IdentityModels;
using ServicesApp.ViewModels.IdentityViewModels;
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
            ServiceProviderProfileViewModel serviceProviderProfileViewModel = _mapper.Map<ServiceProviderProfile, ServiceProviderProfileViewModel>(serviceProviderProfile);
            return serviceProviderProfileViewModel;
        }

        public async Task UpdateServiceProviderProfileAsync(ServiceProviderProfileViewModel serviceProviderProfileViewModel, string userId)
        {
            ServiceProviderProfile serviceProviderProfile = _mapper.Map<ServiceProviderProfileViewModel, ServiceProviderProfile>(serviceProviderProfileViewModel);
            serviceProviderProfile.Id = userId;
            context.ServiceProviderProfiles.AddOrUpdate(serviceProviderProfile);
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}