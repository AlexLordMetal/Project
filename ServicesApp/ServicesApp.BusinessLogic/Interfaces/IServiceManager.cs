using ServicesApp.ViewModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Interfaces
{
    public interface IServiceManager
    {
        Task<List<ShortServiceViewModel>> GetAllAsync();
        Task<List<ShortServiceViewModel>> GetAllApprovedAsync();
        Task<List<ShortServiceViewModel>> GetNotApprovedAsync();
        Task<T> GetByIdAsync<T>(int? id);
        Task AddAsync(CreateServiceViewModel viewModel);
        Task ModifyAsync(CreateServiceViewModel viewModel);
        Task DeleteByIdAsync(int? id);
    }
}