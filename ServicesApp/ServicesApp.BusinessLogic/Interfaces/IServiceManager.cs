using ServicesApp.ViewModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Interfaces
{
    public interface IServiceManager
    {
        Task<ServicesListViewModel> GetListAsync(ServicesSearchModel searchModel);
        Task<T> GetByIdAsync<T>(int? id) where T : ServiceViewModel;
        Task AddAsync(ServiceViewModelCreate viewModel, bool isApproved);
        Task ModifyAsync(ServiceViewModelCreate viewModel, bool isApproved=true);
        Task DeleteByIdAsync(int? id);
        Task<int> NotApprovedCount();
    }
}