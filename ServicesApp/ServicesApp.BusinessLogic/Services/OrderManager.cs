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

        public async Task<List<OrderCustomerViewModel>> GetCustomerOrdersAsync(string userId)
        {
            var datamodel = await context.Orders
                .Where(x=>x.CustomerId==userId)
                .Include(x=>x.ServiceProviderService)
                .Include(x=>x.OrderTime)
                .ToListAsync();
            var viewModel = _mapper.Map<List<OrderCustomerViewModel>>(datamodel);
            return viewModel;
        }


        public void Dispose()
        {
            context.Dispose();
        }
    }
}
