using AutoMapper;
using ServicesApp.BusinessLogic.Interfaces;
using ServicesApp.DataProvider;
using ServicesApp.DataProvider.DataModels;
using ServicesApp.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Services
{
    public class PhotoManager : IPhotoManager, IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        private readonly IMapper _mapper;

        public PhotoManager(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<int> AddAsync(PhotoViewModel viewModel)
        {
            var dataModel = _mapper.Map<Photo>(viewModel);
            context.Photos.Add(dataModel);
            await context.SaveChangesAsync();
            return dataModel.Id;
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
