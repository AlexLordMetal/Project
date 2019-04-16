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

        public async Task<List<ShortServiceViewModel>> GetAllAsync()
        {
            var dataModel = await context.Services.Include(x => x.Category).ToListAsync();
            var viewModel = new List<ShortServiceViewModel>();
            foreach (var item in dataModel)
            {
                viewModel.Add(_mapper.Map<Service, ShortServiceViewModel>(item));
            }
            return viewModel;
        }

        public async Task<List<ShortServiceViewModel>> GetAllApprovedAsync()
        {
            var dataModel = await context.Services.Where(x => x.CategoryId != null).Include(x => x.Category).ToListAsync();
            var viewModel = new List<ShortServiceViewModel>();
            foreach (var item in dataModel)
            {
                viewModel.Add(_mapper.Map<Service, ShortServiceViewModel>(item));
            }
            return viewModel;
        }

        public async Task<List<ShortServiceViewModel>> GetNotApprovedAsync()
        {
            var dataModel = await context.Services.Where(x => x.CategoryId == null).ToListAsync();
            var viewModel = new List<ShortServiceViewModel>();
            foreach (var item in dataModel)
            {
                viewModel.Add(_mapper.Map<Service, ShortServiceViewModel>(item));
            }
            return viewModel;
        }

        public async Task<T> GetByIdAsync<T>(int? id)
        {
            var dataModel = await context.Services.FindAsync(id);
            var viewModel = _mapper.Map<Service, T>(dataModel);
            return viewModel;
        }

        public async Task AddAsync(CreateServiceViewModel viewModel)
        {
            var dataModel = _mapper.Map<CreateServiceViewModel, Service>(viewModel);
            context.Services.Add(dataModel);
            await context.SaveChangesAsync();
        }

        public async Task ModifyAsync(CreateServiceViewModel viewModel)
        {
            if (await context.Services.AnyAsync(x => x.Id == viewModel.Id))
            {
                var dataModel = _mapper.Map<CreateServiceViewModel, Service>(viewModel);
                context.Services.Attach(dataModel);
                context.Entry<Service>(dataModel).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteByIdAsync(int? id)
        {
            var dataModel = await context.ServiceCategories.FindAsync(id);
            if (dataModel != null)
            {
                context.ServiceCategories.Remove(dataModel);
                await context.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
