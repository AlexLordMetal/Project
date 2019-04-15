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

        public async Task<List<ShortServiceCategoryViewModel>> GetAllAsync()
        {
            var dataModel = await context.ServiceCategories.ToListAsync();
            var viewModel = new List<ShortServiceCategoryViewModel>();
            foreach (var item in dataModel)
            {
                viewModel.Add(_mapper.Map<ServiceCategory, ShortServiceCategoryViewModel>(item));
            }
            return viewModel;
        }

        public async Task<FullServiceCategoryViewModel> GetByIdAsync(int? id)
        {
            var dataModel = await context.ServiceCategories.FindAsync(id);
            var viewModel = _mapper.Map<ServiceCategory, FullServiceCategoryViewModel>(dataModel);
            return viewModel;
        }

        public async Task<ShortServiceCategoryViewModel> GetShortByIdAsync(int? id)
        {
            var dataModel = await context.ServiceCategories.FindAsync(id);
            var viewModel = _mapper.Map<ServiceCategory, ShortServiceCategoryViewModel>(dataModel);
            return viewModel;
        }

        public async Task AddAsync(ShortServiceCategoryViewModel viewModel)
        {
            var dataModel = _mapper.Map<ShortServiceCategoryViewModel, ServiceCategory>(viewModel);
            context.ServiceCategories.Add(dataModel);
            await context.SaveChangesAsync();
        }

//Does it sovsem dich? Не работает
        public async Task ModifyAsync(ShortServiceCategoryViewModel viewModel)
        {
            //if (await context.ServiceCategories.FindAsync(viewModel.Id) != null)
            //{
                var dataModel = _mapper.Map<ShortServiceCategoryViewModel, ServiceCategory>(viewModel);
                context.ServiceCategories.Attach(dataModel);
                context.Entry<ServiceCategory>(dataModel).State = EntityState.Modified;
                await context.SaveChangesAsync();
            //}
        }

        public async Task DeleteByIdAsync(int? id)
        {
            var dataModel = await context.ServiceCategories.FindAsync(id);
            if (dataModel != null)
            {
                //doesn't work if any services are in this category
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
