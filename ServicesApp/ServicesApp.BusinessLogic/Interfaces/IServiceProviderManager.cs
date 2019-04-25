using ServicesApp.ViewModels.IdentityViewModels;
using ServicesApp.ViewModels.ViewModels;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Interfaces
{
    public interface IServiceProviderManager
    {
        Task UpdateServiceProviderProfileAsync(ServiceProviderProfileViewModel serviceProviderProfileViewModel, string userId);
        Task<ServiceProviderProfileViewModel> GetServiceProviderProfileAsync(string userId);
        Task<ServiceProviderServiceRelationViewModel> GetServiceRelationAsync(string serviceProviderId, int serviceId);
        Task AddOrUpdateServiceRelationAsync(ServiceProviderServiceRelationViewModel viewModel);
        Task DeleteServiceRelationAsync(ServiceProviderServiceRelationDeleteViewModel viewModel);
    }
}