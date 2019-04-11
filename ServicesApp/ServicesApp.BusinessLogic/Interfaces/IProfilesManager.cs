using ServicesApp.DataProvider.IdentityModels;
using ServicesApp.ViewModels.IdentityViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Interfaces
{
    public interface IProfilesManager
    {
        Task UpdateCustomerProfileAsync(CustomerProfileViewModel customerProfileViewModel, string userId);
        Task<CustomerProfileViewModel> GetCustomerProfileAsync(string userId);
    }
}