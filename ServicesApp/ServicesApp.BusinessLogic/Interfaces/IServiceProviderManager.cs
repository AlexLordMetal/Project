﻿using ServicesApp.ViewModels.IdentityViewModels;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Interfaces
{
    public interface IServiceProviderManager
    {
        Task UpdateServiceProviderProfileAsync(ServiceProviderProfileViewModel viewModel, string userId);
        Task<ServiceProviderProfileViewModel> GetServiceProviderProfileAsync(string userId);
        Task<bool> IsServiceProviderProfileExistAsync(string userId);
    }
}