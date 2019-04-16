using ServicesApp.ViewModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Interfaces
{
    public interface IServiceManager
    {
        Task<List<ShortServiceViewModel>> GetAllAsync();
        Task<FullServiceViewModel> GetByIdAsync(int? id);
        Task<ShortServiceViewModel> GetShortByIdAsync(int? id);
        Task AddAsync(CreateServiceViewModel viewModel);
        //Task ModifyAsync(ShortServiceViewModel viewModel);
        //Task DeleteByIdAsync(int? id);
    }
}