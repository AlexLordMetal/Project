using ServicesApp.ViewModels.IdentityViewModels;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Interfaces
{
    public interface ICustomerManager
    {
        Task UpdateCustomerProfileAsync(CustomerProfileViewModel viewModel, string userId);
        Task<bool> IsCustomerProfileExistAsync(string userId);
        Task<CustomerProfileViewModel> GetCustomerProfileAsync(string userId);
    }
}
