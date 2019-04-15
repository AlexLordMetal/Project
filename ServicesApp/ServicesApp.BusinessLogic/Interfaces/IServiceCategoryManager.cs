using ServicesApp.ViewModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Interfaces
{
    public interface IServiceCategoryManager
    {
        Task<List<ShortServiceCategoryViewModel>> GetAllAsync();
        Task<FullServiceCategoryViewModel> GetByIdAsync(int? id);
        Task<ShortServiceCategoryViewModel> GetShortByIdAsync(int? id);
        Task AddAsync(ShortServiceCategoryViewModel shortServiceCategoryViewModel);
        Task ModifyAsync(ShortServiceCategoryViewModel shortServiceCategoryViewModel);
        Task DeleteByIdAsync(int? id);
    }
}
