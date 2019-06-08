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
    public class OrderManager : IDisposable, IOrderManager
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        private readonly IMapper _mapper;

        public OrderManager(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<List<T>> GetCustomerOrdersAsync<T>(string userId) where T : OrderViewModelShort
        {
            var dataModel = await context.Orders
                .Where(x => x.CustomerId == userId)
                .Include(x => x.ServiceProviderService)
                .Include(x => x.Customer)
                .ToListAsync();
            var viewModel = _mapper.Map<List<T>>(dataModel);
            return viewModel;
        }

        public async Task<List<T>> GetServiceProviderOrdersAsync<T>(string userId) where T : OrderViewModelShort
        {
            var dataModel = await context.Orders
                .Where(x => x.ServiceProviderService.ServiceProviderId == userId)
                .Include(x => x.ServiceProviderService)
                .Include(x => x.Customer)
                .ToListAsync();
            var viewModel = _mapper.Map<List<T>>(dataModel);
            return viewModel;
        }

        public async Task<T> GetOrderByIdAsync<T>(int id) where T : OrderViewModelShort
        {
            var dataModel = await context.Orders.FindAsync(id);
            var viewModel = _mapper.Map<T>(dataModel);
            return viewModel;
        }

        public async Task CreateOrderAsync(OrderViewModelShort viewModel, string customerId)
        {
            var dataModel = _mapper.Map<Order>(viewModel);
            dataModel.CustomerId = customerId;
            context.Orders.Add(dataModel);
            await context.SaveChangesAsync();
        }

        public async Task ConfirmOrderAsync(int id, string serviceProviderId)
        {
            var dataModel = await context.Orders.FindAsync(id);
            if (dataModel.ServiceProviderService.ServiceProviderId == serviceProviderId)
            {
                dataModel.ServiceProviderConfirm = true;
                await context.SaveChangesAsync();
            }
        }

        public async Task ModifyAsync(OrderViewModelShort viewModel)
        {
            if (await context.Orders.AnyAsync(x => x.Id == viewModel.Id))
            {
                var dataModel = _mapper.Map<Order>(viewModel);
                context.Orders.Attach(dataModel);
                context.Entry<Order>(dataModel).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }

        public async Task<int> NotConfirmedCount(string userId)
        {
            return await context.Orders.Where(x => x.ServiceProviderService.ServiceProviderId == userId)
                .CountAsync(x => x.ServiceProviderConfirm == false);
        }

        public async Task<int> NotCompletedCount(string userId)
        {
            return await context.Orders.Where(x => x.CustomerId == userId)
                .Where(x => x.ServiceProviderConfirm == true)
                .Where(x => x.OrderDate < DateTime.Now)
                .CountAsync(x => x.IsComplete == false);
        }

        public async Task<List<DateTime>> GetExcludedDatesAsync(string serviceProviderId)
        {
            var excludedDates = new List<DateTime>();
            excludedDates.AddRange(await context.Orders
                .Where(x => x.ServiceProviderService.ServiceProviderId == serviceProviderId)
                .Select(x => x.OrderDate).Where(x => x >= DateTime.Now)
                .ToListAsync()
                );
            return excludedDates;
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
