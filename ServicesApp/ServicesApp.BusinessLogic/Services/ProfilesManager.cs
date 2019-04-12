using AutoMapper;
using ServicesApp.BusinessLogic.Interfaces;
using ServicesApp.DataProvider;
using ServicesApp.DataProvider.IdentityModels;
using ServicesApp.ViewModels.IdentityViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Services
{
    public class ProfilesManager : IProfilesManager, IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        private readonly IMapper _mapper;

        public ProfilesManager(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<CustomerProfileViewModel> GetCustomerProfileAsync(string userId)
        {
            var customerProfile = await context.CustomerProfiles.FindAsync(userId);
            CustomerProfileViewModel customerProfileViewModel = _mapper.Map<CustomerProfile, CustomerProfileViewModel>(customerProfile);
            return customerProfileViewModel;
        }

        public async Task UpdateCustomerProfileAsync(CustomerProfileViewModel customerProfileViewModel, string userId)
        {
            CustomerProfile customerProfile = _mapper.Map<CustomerProfileViewModel, CustomerProfile>(customerProfileViewModel);
            customerProfile.Id = userId;
            context.CustomerProfiles.AddOrUpdate(customerProfile);
            await context.SaveChangesAsync();
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
