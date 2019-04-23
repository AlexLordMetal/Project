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

        public async Task<ServicesListViewModel> GetListAsync(bool isApproved, int pageNumber, int itemsPerPage, string searchString)
        {
            var pageInfo = new PageInfoViewModel();
            pageInfo.PageNumber = pageNumber;
            pageInfo.PageSize = itemsPerPage;

            List<Service> dataModel = null;

            if (!String.IsNullOrWhiteSpace(searchString))
            {
                pageInfo.TotalItems = await context.Services
                    .Where(x => x.IsApproved == isApproved)
                    .Where(x => x.Name.Contains(searchString))
                    .CountAsync();

                dataModel = await context.Services
                    .Where(x => x.IsApproved == isApproved)
                    .Where(x => x.Name.Contains(searchString))
                    .OrderBy(x => x.Id)
                    .Skip(pageInfo.SkipItems)
                    .Take(pageInfo.PageSize)
                    .Include(x => x.Category)
                    .ToListAsync();
            }
            else
            {
                pageInfo.TotalItems = await context.Services
                    .Where(x => x.IsApproved == isApproved)
                    .CountAsync();

                dataModel = await context.Services
                    .Where(x => x.IsApproved == isApproved)
                    .OrderBy(x => x.Id)
                    .Skip(pageInfo.SkipItems)
                    .Take(pageInfo.PageSize)
                    .Include(x => x.Category)
                    .ToListAsync();
            }
            var viewModel = new ServicesListViewModel();
            viewModel.Services = _mapper.Map<List<ServiceViewModelFull>>(dataModel);
            viewModel.PageInfo = pageInfo;
            return viewModel;
        }

        public async Task<T> GetByIdAsync<T>(int? id) where T : ServiceViewModelShort
        {
            var dataModel = await context.Services.FindAsync(id);
            var viewModel = _mapper.Map<T>(dataModel);
            return viewModel;
        }

        public async Task AddAsync(ServiceViewModelCreateShort viewModel, bool isApproved)
        {
            var dataModel = _mapper.Map<Service>(viewModel);
            dataModel.IsApproved = isApproved;
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
