using ServicesApp.DataProvider.DataModels;
using ServicesApp.ViewModels.ViewModels;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Interfaces
{
    public interface IPhotoManager
    {
        Task<int> AddAsync(PhotoViewModel viewModel);
    }
}