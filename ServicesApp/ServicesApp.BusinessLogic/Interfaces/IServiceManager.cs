﻿using ServicesApp.ViewModels.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Interfaces
{
    public interface IServiceManager
    {
        Task<ServicesListViewModel> GetListAsync(bool isApproved, int pageNumber, int itemsPerPage, string searchString=null);
        Task<T> GetByIdAsync<T>(int? id) where T : ServiceViewModelShort;
        Task AddAsync(ServiceViewModelCreateShort viewModel, bool isApproved);
        Task ModifyAsync(ServiceViewModelCreateShort viewModel, bool isApproved=true);
        Task DeleteByIdAsync(int? id);
    }
}