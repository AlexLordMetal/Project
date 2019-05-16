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
    public class CustomerManager : ICustomerManager, IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        private readonly IMapper _mapper;

        public CustomerManager(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<CustomerProfileViewModel> GetCustomerProfileAsync(string userId)
        {
            var customerProfile = await context.CustomerProfiles.FindAsync(userId);
            var customerProfileViewModel = _mapper.Map<CustomerProfile, CustomerProfileViewModel>(customerProfile);
            return customerProfileViewModel;
        }

        public async Task<bool> IsCustomerProfileExistAsync(string userId)
        {
            if (await context.CustomerProfiles.FindAsync(userId) != null)
            {
                return true;
            }
            return false;
        }

        public async Task UpdateCustomerProfileAsync(CustomerProfileViewModel customerProfileViewModel, string userId)
        {
            CustomerProfile customerProfile = _mapper.Map<CustomerProfileViewModel, CustomerProfile>(customerProfileViewModel);
            customerProfile.Id = userId;
            context.CustomerProfiles.AddOrUpdate(customerProfile);
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
