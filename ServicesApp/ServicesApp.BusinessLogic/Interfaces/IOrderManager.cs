using ServicesApp.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Interfaces
{
    public interface IOrderManager
    {
        Task<List<T>> GetCustomerOrdersAsync<T>(string userId) where T : OrderViewModelCustomer;
        Task<List<T>> GetServiceProviderOrdersAsync<T>(string userId) where T : OrderViewModelCustomer;
        Task<T> GetOrderByIdAsync<T>(int id) where T : OrderViewModelCustomer;
        Task CreateOrderAsync(OrderViewModelShort viewModel);
        Task ConfirmOrderAsync(int id, string customerId);
        Task CompleteAsync(OrderViewModelCustomer viewModel);
        Task<int> NotConfirmedCount(string userId);
        Task<int> NotCompletedCount(string userId);
        Task<List<DateTime>> GetExcludedDatesAsync(string serviceProviderId);
    }
}