using AutoMapper;
using ServicesApp.BusinessLogic.Interfaces;
using ServicesApp.DataProvider;
using ServicesApp.DataProvider.IdentityModels;
using ServicesApp.ViewModels.IdentityViewModels;
using System;
using System.Data.Entity;
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
            var dataModel = await context.CustomerProfiles.FindAsync(userId);
            var viewModel = _mapper.Map<CustomerProfileViewModel>(dataModel);
            return viewModel;
        }

        public async Task<bool> IsCustomerProfileExistAsync(string userId)
        {
            if (await context.CustomerProfiles.FindAsync(userId) != null)
            {
                return true;
            }
            return false;
        }

        public async Task UpdateCustomerProfileAsync(CustomerProfileViewModel viewModel, string userId)
        {
            var dataModel = _mapper.Map<CustomerProfile>(viewModel);
            dataModel.Id = userId;
            if (await context.CustomerProfiles.AnyAsync(x => x.Id == dataModel.Id))
            {
                context.CustomerProfiles.Attach(dataModel);
                context.Entry<CustomerProfile>(dataModel).State = EntityState.Modified;
            }
            else
            {
                context.CustomerProfiles.Add(dataModel);
            }
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
