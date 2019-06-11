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
            var dataModel = await context.ServiceProviderProfiles.FindAsync(userId);
            var viewModel = _mapper.Map<ServiceProviderProfileViewModel>(dataModel);
            return viewModel;
        }

        public async Task<bool> IsServiceProviderProfileExistAsync(string userId)
        {
            if (await context.ServiceProviderProfiles.FindAsync(userId) != null)
            {
                return true;
            }
            return false;
        }

        public async Task UpdateServiceProviderProfileAsync(ServiceProviderProfileViewModel viewModel, string userId)
        {
            var dataModel = _mapper.Map<ServiceProviderProfile>(viewModel);
            dataModel.Id = userId;
            if (await context.ServiceProviderProfiles.AnyAsync(x => x.Id == dataModel.Id))
            {
                context.ServiceProviderProfiles.Attach(dataModel);
                context.Entry<ServiceProviderProfile>(dataModel).State = EntityState.Modified;
            }
            else
            {
                context.ServiceProviderProfiles.Add(dataModel);
            }
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}