using AutoMapper;
using ServicesApp.BusinessLogic;
using ServicesApp.BusinessLogic.Interfaces;
using ServicesApp.BusinessLogic.Services;
using System;

using Unity;

namespace ServicesApp.Website
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public static class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container =
          new Lazy<IUnityContainer>(() =>
          {
              var container = new UnityContainer();
              RegisterTypes(container);
              return container;
          });

        /// <summary>
        /// Configured Unity Container.
        /// </summary>
        public static IUnityContainer Container => container.Value;
        #endregion

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ICustomerManager, CustomerManager>();
            container.RegisterType<IServiceProviderManager, ServiceProviderManager>();
            container.RegisterType<IServiceCategoryManager, ServiceCategoryManager>();
            container.RegisterType<IServiceManager, ServiceManager>();
            container.RegisterType<IProviderServiceRelationManager, ProviderServiceRelationManager>();
            container.RegisterType<IOrderManager, OrderManager>();
            container.RegisterType<IPhotoManager, PhotoManager>();

            // Initialize AutoMapper
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            container.RegisterInstance<IMapper>(config.CreateMapper());
        }
    }
}