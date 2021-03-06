﻿using ServicesApp.ViewModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Interfaces
{
    public interface IProviderServiceRelationManager
    {
        Task<bool> IsRelationExistsAsync(string serviceProviderId, int serviceId);
        Task<T> GetServiceRelationAsync<T>(string serviceProviderId, int serviceId) where T : ProviderServiceFullViewModel;
        Task<T> GetServiceRelationAsync<T>(int Id) where T : ProviderServiceFullViewModel;
        Task<List<ProviderServiceFullViewModel>> GetServiceProviderServicesAsync(string UserId);
        Task CreateServiceRelationAsync(ProviderServiceRelationViewModel viewModel);
        Task ModifyServiceRelationAsync(ProviderServiceCreateViewModel viewModel);
        Task DeleteServiceRelationAsync(int id);
    }
}
