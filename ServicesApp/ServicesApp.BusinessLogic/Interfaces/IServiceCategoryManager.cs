using ServicesApp.ViewModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Interfaces
{
    public interface IServiceCategoryManager
    {
        Task<List<ServiceCategoryViewModelShort>> GetAllAsync();
        Task<T> GetByIdAsync<T>(int? id) where T : ServiceCategoryViewModelShort;
        Task AddAsync(ServiceCategoryViewModelShort viewModel);
        Task ModifyAsync(ServiceCategoryViewModelShort viewModel);
        Task DeleteByIdAsync(int? id);
    }
}