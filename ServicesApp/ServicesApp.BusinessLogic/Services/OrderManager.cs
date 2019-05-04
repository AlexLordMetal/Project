using AutoMapper;
using ServicesApp.DataProvider;
using ServicesApp.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Services
{
    class OrderManager : IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        private readonly IMapper _mapper;

        public OrderManager(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<List<T>> GetOrdersAsync<T>(string userId) where T : OrderViewModelShort
        {
            var datamodel = await context.Orders
                .Where(x => x.CustomerId == userId)
                .Include(x => x.ServiceProviderService)
                .Include(x => x.Customer)
                .Include(x => x.OrderTime)
                .ToListAsync();
            var viewModel = _mapper.Map<List<T>>(datamodel);
            return viewModel;
        }

        public async Task<T> GetOrderByIdAsync<T>(int id) where T : OrderViewModelShort
        {
            var datamodel = await context.Orders.FindAsync(id);
            var viewModel = _mapper.Map<T>(datamodel);
            return viewModel;
        }

        public async Task CreateOrderAsync(OrderViewModelShort viewModel, string customerId)
        {

        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
