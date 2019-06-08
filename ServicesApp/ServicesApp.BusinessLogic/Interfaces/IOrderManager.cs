using ServicesApp.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Interfaces
{
    public interface IOrderManager
    {
        Task<List<T>> GetCustomerOrdersAsync<T>(string userId) where T : OrderViewModelShort;
        Task<List<T>> GetServiceProviderOrdersAsync<T>(string userId) where T : OrderViewModelShort;
        Task<T> GetOrderByIdAsync<T>(int id) where T : OrderViewModelShort;
        Task CreateOrderAsync(OrderViewModelShort viewModel, string customerId);
        Task ConfirmOrderAsync(int id, string customerId);
        Task ModifyAsync(OrderViewModelShort viewModel);
        Task<int> NotConfirmedCount(string userId);
        Task<int> NotCompletedCount(string userId);
        Task<List<DateTime>> GetExcludedDatesAsync(string serviceProviderId);
    }
}