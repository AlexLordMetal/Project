using ServicesApp.ViewModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Interfaces
{
    public interface IServiceManager
    {
        Task<List<ServiceViewModelFull>> GetAllAsync();
        Task<List<ServiceViewModelFull>> GetAllApprovedAsync();
        Task<List<ServiceViewModelFull>> GetNotApprovedAsync();
        Task<T> GetByIdAsync<T>(int? id) where T : ServiceViewModelShort;
        Task AddAsync(ServiceViewModelCreateShort viewModel);
        Task ModifyAsync(ServiceViewModelCreateShort viewModel);
        Task DeleteByIdAsync(int? id);
    }
}