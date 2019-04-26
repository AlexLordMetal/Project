﻿using AutoMapper;
using ServicesApp.BusinessLogic.Interfaces;
using ServicesApp.DataProvider;
using ServicesApp.DataProvider.DataModels;
using ServicesApp.DataProvider.IdentityModels;
using ServicesApp.ViewModels.IdentityViewModels;
using ServicesApp.ViewModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesApp.BusinessLogic.Services
{
    public class ServiceProviderManager : IServiceProviderManager, IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        private readonly IMapper _mapper;

        public ServiceProviderManager(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceProviderProfileViewModel> GetServiceProviderProfileAsync(string userId)
        {
            var serviceProviderProfile = await context.ServiceProviderProfiles.FindAsync(userId);
            ServiceProviderProfileViewModel serviceProviderProfileViewModel = _mapper.Map<ServiceProviderProfileViewModel>(serviceProviderProfile);
            return serviceProviderProfileViewModel;
        }

        public async Task UpdateServiceProviderProfileAsync(ServiceProviderProfileViewModel serviceProviderProfileViewModel, string userId)
        {
            ServiceProviderProfile serviceProviderProfile = _mapper.Map<ServiceProviderProfile>(serviceProviderProfileViewModel);
            serviceProviderProfile.Id = userId;
            context.ServiceProviderProfiles.AddOrUpdate(serviceProviderProfile);
            await context.SaveChangesAsync();
        }

        public async Task<ServiceProviderServiceFullViewModel> GetServiceRelationAsync(string serviceProviderId, int serviceId)
        {
            var dataModel = await context.ServiceProviderServices.FindAsync(serviceProviderId, serviceId);
            var viewModel = _mapper.Map<ServiceProviderServiceFullViewModel>(dataModel);
            return viewModel;
        }

        public async Task<List<ServiceProviderServiceFullViewModel>> GetServiceProviderServicesAsync(string UserId)
        {
            var dataModel = await context.ServiceProviderServices
                .Where(x => x.ServiceProviderId == UserId)
                .Include(x => x.Service)
                .Include(x => x.Service.Category)
                .ToListAsync();
            var viewModel =_mapper.Map<List<ServiceProviderServiceFullViewModel>>(dataModel);
            return viewModel;
        }

        public async Task AddOrUpdateServiceRelationAsync(ServiceProviderServiceRelationViewModel viewModel)
        {
            var dataModel = _mapper.Map<ServiceProviderService>(viewModel);
            context.ServiceProviderServices.AddOrUpdate(dataModel);
            await context.SaveChangesAsync();
        }

        public async Task DeleteServiceRelationAsync(string serviceProviderId, int serviceId)
        {
            var dataModel = await context.ServiceProviderServices.FindAsync(serviceProviderId, serviceId);
            context.ServiceProviderServices.Remove(dataModel);
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}