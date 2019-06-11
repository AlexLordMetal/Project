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

        private IPhotoManager _photoManager;

        public ServiceManager(IMapper mapper, IPhotoManager photoManager)
        {
            _mapper = mapper;
            _photoManager = photoManager;
        }

        public async Task<ServicesListViewModel> GetListAsync(ServicesSearchModel searchModel)
        {
            var pageInfo = new PageInfoViewModel();
            pageInfo.PageNumber = searchModel.Page;
            pageInfo.PageSize = searchModel.ItemsPerPage;

            var getServices = context.Services.Where(x => x.IsApproved == searchModel.IsApproved);

            if (!String.IsNullOrWhiteSpace(searchModel.Search))
            {
                getServices = getServices.Where(x => x.Name.Contains(searchModel.Search));
            }
            if (searchModel.CategoryId != null)
            {
                getServices = getServices.Where(x => x.CategoryId == searchModel.CategoryId);
            }
            if (searchModel.SortAscending == true)
            {
                getServices = getServices.OrderBy(x => x.Name);
            }
            else
            {
                getServices = getServices.OrderByDescending(x => x.Name);
            }

            pageInfo.TotalItems = await getServices.CountAsync();

            var dataModel = await getServices
                .Skip(pageInfo.SkipItems)
                .Take(pageInfo.PageSize)
                .Include(x => x.Category)
                .ToListAsync();

            var viewModel = new ServicesListViewModel();
            viewModel.Services = _mapper.Map<List<ServiceViewModel>>(dataModel);
            viewModel.PageInfo = pageInfo;
            return viewModel;
        }

        public async Task<T> GetByIdAsync<T>(int? id) where T : ServiceViewModel
        {
            var dataModel = await context.Services.FindAsync(id);
            var viewModel = _mapper.Map<T>(dataModel);
            return viewModel;
        }

        public async Task AddAsync(ServiceViewModelCreate viewModel, bool isApproved)
        {
            var dataModel = _mapper.Map<Service>(viewModel);
            if (viewModel.Photo != null)
            {
                dataModel.PhotoId = await _photoManager.AddAsync(viewModel.Photo);
            }
            dataModel.IsApproved = isApproved;
            context.Services.Add(dataModel);
            await context.SaveChangesAsync();
        }

        public async Task ModifyAsync(ServiceViewModelCreate viewModel, bool isApproved=true)
        {
            if (await context.Services.AnyAsync(x => x.Id == viewModel.Id))
            {
                var dataModel = _mapper.Map<Service>(viewModel);
                if (viewModel.Photo != null)
                {
                    dataModel.PhotoId = await _photoManager.AddAsync(viewModel.Photo);
                }
                dataModel.IsApproved = isApproved;
                context.Services.Attach(dataModel);
                context.Entry<Service>(dataModel).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception();
            }
        }

        public async Task<int> NotApprovedCount()
        {
            return await context.Services.CountAsync(x => x.IsApproved == false);
        }

        public async Task DeleteByIdAsync(int? id)
        {
            var dataModel = await context.Services.FindAsync(id);
            if (dataModel != null)
            {
                context.Services.Remove(dataModel);
                await context.SaveChangesAsync();
            }
            else
            {
                throw new Exception();
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
