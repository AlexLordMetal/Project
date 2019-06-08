using AutoMapper;
using ServicesApp.BusinessLogic.Interfaces;
using ServicesApp.DataProvider;
using ServicesApp.DataProvider.DataModels;
using ServicesApp.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Services
{
    public class ProviderServiceRelationManager : IProviderServiceRelationManager, IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        private readonly IMapper _mapper;
        private IPhotoManager _photoManager;

        public ProviderServiceRelationManager(IMapper mapper, IPhotoManager photoManager)
        {
            _mapper = mapper;
            _photoManager = photoManager;
        }

        public async Task<bool> IsRelationExistsAsync(string serviceProviderId, int serviceId)
        {
            return await context.ServiceProviderServices.AnyAsync(x => (x.ServiceProviderId == serviceProviderId) & (x.ServiceId == serviceId));
        }
        public async Task<T> GetServiceRelationAsync<T>(string serviceProviderId, int serviceId) where T : ProviderServiceFullViewModel
        {
            var dataModel = await context.ServiceProviderServices.FirstOrDefaultAsync(x => (x.ServiceProviderId == serviceProviderId) & (x.ServiceId == serviceId));
            var viewModel = _mapper.Map<T>(dataModel);
            return viewModel;
        }

        public async Task<T> GetServiceRelationAsync<T>(int id) where T : ProviderServiceFullViewModel
        {
            var dataModel = await context.ServiceProviderServices.FindAsync(id);
            var viewModel = _mapper.Map<T>(dataModel);
            return viewModel;
        }

        public async Task<List<ProviderServiceFullViewModel>> GetServiceProviderServicesAsync(string userId)
        {
            var dataModel = await context.ServiceProviderServices
                .Where(x => x.ServiceProviderId == userId)
                .Include(x => x.Service)
                .Include(x => x.Service.Category)
                .ToListAsync();
            var viewModel = _mapper.Map<List<ProviderServiceFullViewModel>>(dataModel);
            return viewModel;
        }

        public async Task CreateServiceRelationAsync(ProviderServiceRelationViewModel viewModel)
        {
            var dataModel = _mapper.Map<ServiceProviderService>(viewModel);
            if (viewModel.Photo != null)
            {
                dataModel.PhotoId = await _photoManager.AddAsync(viewModel.Photo);
            }
            context.ServiceProviderServices.Add(dataModel);
            await context.SaveChangesAsync();
        }

        public async Task ModifyServiceRelationAsync(ProviderServiceCreateViewModel viewModel)
        {
            if (await context.Services.AnyAsync(x => x.Id == viewModel.Id))
            {
                var dataModel = _mapper.Map<ServiceProviderService>(viewModel);
                if (viewModel.Photo != null)
                {
                    dataModel.PhotoId = await _photoManager.AddAsync(viewModel.Photo);
                }
                context.ServiceProviderServices.Attach(dataModel);
                context.Entry<ServiceProviderService>(dataModel).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            //Need exception "Id not found" or something else
        }

        public async Task DeleteServiceRelationAsync(int id)
        {
            var dataModel = await context.ServiceProviderServices.FindAsync(id);
            context.ServiceProviderServices.Remove(dataModel);
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}