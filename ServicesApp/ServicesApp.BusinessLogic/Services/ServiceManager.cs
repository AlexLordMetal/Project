using AutoMapper;
using ServicesApp.BusinessLogic.Interfaces;
using ServicesApp.DataProvider;
using ServicesApp.DataProvider.DataModels;
using ServicesApp.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Services
{
    public class ServiceManager : IServiceManager, IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        private readonly IMapper _mapper;

        public ServiceManager(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<List<ServiceViewModelFull>> GetAllAsync()
        {
            var dataModel = await context.Services.Include(x => x.Category).ToListAsync();
            var viewModel = _mapper.Map<List<ServiceViewModelFull>>(dataModel);
            return viewModel;
        }

        public async Task<List<ServiceViewModelFull>> GetAllApprovedAsync()
        {
            var dataModel = await context.Services.Where(x => x.CategoryId != null).Include(x => x.Category).ToListAsync();
            var viewModel = _mapper.Map<List<ServiceViewModelFull>>(dataModel);
            return viewModel;
        }

        public async Task<List<ServiceViewModelFull>> GetNotApprovedAsync()
        {
            var dataModel = await context.Services.Where(x => x.CategoryId == null).ToListAsync();
            var viewModel = _mapper.Map<List<ServiceViewModelFull>>(dataModel);
            return viewModel;
        }

        public async Task<T> GetByIdAsync<T>(int? id) where T : ServiceViewModelShort
        {
            var dataModel = await context.Services.FindAsync(id);
            var viewModel = _mapper.Map<T>(dataModel);
            return viewModel;
        }

        public async Task AddAsync(ServiceViewModelCreateShort viewModel)
        {
            var dataModel = _mapper.Map<Service>(viewModel);
            context.Services.Add(dataModel);
            await context.SaveChangesAsync();
        }

        public async Task ModifyAsync(ServiceViewModelCreateShort viewModel)
        {
            if (await context.Services.AnyAsync(x => x.Id == viewModel.Id))
            {
                var dataModel = _mapper.Map<Service>(viewModel);
                context.Services.Attach(dataModel);
                context.Entry<Service>(dataModel).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            //Need exception "Id not found" or something else
        }

        public async Task DeleteByIdAsync(int? id)
        {
            var dataModel = await context.Services.FindAsync(id);
            if (dataModel != null)
            {
                context.Services.Remove(dataModel);
                await context.SaveChangesAsync();
            }
            //Need exception "Id not found" or something else
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
