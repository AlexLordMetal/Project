using ServicesApp.ViewModels.IdentityViewModels;
using ServicesApp.ViewModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Interfaces
{
    public interface IServiceProviderManager
    {
        Task UpdateServiceProviderProfileAsync(ServiceProviderProfileViewModel serviceProviderProfileViewModel, string userId);
        Task<ServiceProviderProfileViewModel> GetServiceProviderProfileAsync(string userId);
        Task<ServiceProviderServiceFullViewModel> GetServiceRelationAsync(string serviceProviderId, int serviceId);
        Task<ServiceProviderServiceFullViewModel> GetServiceRelationAsync(int Id);
        Task<List<ServiceProviderServiceFullViewModel>> GetServiceProviderServicesAsync(string UserId);
        Task AddOrUpdateServiceRelationAsync(ServiceProviderServiceRelationViewModel viewModel);
        Task DeleteServiceRelationAsync(int id);
    }
}