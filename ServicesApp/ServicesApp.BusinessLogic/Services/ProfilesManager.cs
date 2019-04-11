using AutoMapper;
using ServicesApp.BusinessLogic.Interfaces;
using ServicesApp.DataProvider;
using ServicesApp.DataProvider.IdentityModels;
using ServicesApp.ViewModels.IdentityViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Services
{
    public class ProfilesManager : IProfilesManager, IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public async Task<CustomerProfileViewModel> GetCustomerProfileAsync(string userId)
        {
            var customerProfile = await context.CustomerProfiles.FindAsync(userId);
            Mapper.Initialize(cfg => cfg.CreateMap<CustomerProfile, CustomerProfileViewModel>());
            CustomerProfileViewModel customerProfileViewModel = Mapper.Map<CustomerProfile, CustomerProfileViewModel>(customerProfile);
            return customerProfileViewModel;
        }

        public async Task UpdateCustomerProfileAsync(CustomerProfileViewModel customerProfileViewModel, string userId)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<CustomerProfileViewModel, CustomerProfile>()
                .ForMember("Id", opt => opt.MapFrom(userId)));
            CustomerProfile customerProfile = Mapper.Map<CustomerProfileViewModel, CustomerProfile>(customerProfileViewModel);
            if (context.CustomerProfiles.FindAsync(userId) != null)
            {
                context.Entry(customerProfile).State = EntityState.Modified;
            }
            else
            {
                context.CustomerProfiles.Add(customerProfile);
            }
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
