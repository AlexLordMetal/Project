using ServicesApp.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Interfaces
{
    public interface IOrderManager
    {
        Task<List<T>> GetCustomerOrdersAsync<T>(string userId) where T : OrderViewModelShort;
        Task<T> GetOrderByIdAsync<T>(int id) where T : OrderViewModelShort;
        Task CreateOrderAsync(OrderViewModelShort viewModel, string customerId);
    }
}
