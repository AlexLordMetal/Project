using AutoMapper;
using ServicesApp.BusinessLogic.Interfaces;
using ServicesApp.DataProvider;
using ServicesApp.DataProvider.DataModels;
using ServicesApp.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Services
{
    public class ServiceCategoryManager : IServiceCategoryManager, IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        private readonly IMapper _mapper;

        public ServiceCategoryManager(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<List<ServiceCategoryViewModelShort>> GetAllAsync()
        {
            var dataModel = await context.ServiceCategories.ToListAsync();
            var viewModel = _mapper.Map<List<ServiceCategoryViewModelShort>>(dataModel);
            return viewModel;
        }

        public async Task<T> GetByIdAsync<T>(int? id) where T : ServiceCategoryViewModelShort
        {
            var dataModel = await context.ServiceCategories.FindAsync(id);
            var viewModel = _mapper.Map<T>(dataModel);
            return viewModel;
        }

        public async Task AddAsync(ServiceCategoryViewModelShort viewModel)
        {
            var dataModel = _mapper.Map<ServiceCategory>(viewModel);
            context.ServiceCategories.Add(dataModel);
            await context.SaveChangesAsync();
        }

        public async Task ModifyAsync(ServiceCategoryViewModelShort viewModel)
        {
            if (await context.ServiceCategories.AnyAsync(x=>x.Id==viewModel.Id))
            {
                var dataModel = _mapper.Map<ServiceCategory>(viewModel);
                context.ServiceCategories.Attach(dataModel);
                context.Entry<ServiceCategory>(dataModel).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            //Need exception "Id not found" or something else
        }

        public async Task DeleteByIdAsync(int? id)
        {
            var dataModel = await context.ServiceCategories.FindAsync(id);
            if (dataModel != null)
            {
                foreach (var service in dataModel.Services)
                {
                    service.CategoryId = null;
                }
                context.ServiceCategories.Remove(dataModel);
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
