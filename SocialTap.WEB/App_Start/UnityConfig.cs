using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SocialTap.Contract.DataContracts;
using SocialTap.Contract.Repositories;
using SocialTap.Contract.Services;
using SocialTap.DataAccess.Models;
using SocialTap.DataAccess.Repositories;
using SocialTap.Services.Services;
using SocialTap.WEB.Controllers;
using SocialTap.WEB.Models;
using System;
using System.Web.Mvc;
using Unity;
using Unity.AspNet.Mvc;
using Unity.Injection;

namespace SocialTap.WEB
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

        /// <summary>
        /// Registers the type mappings with the Unity container.
        /// </summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>
        /// There is no need to register concrete types such as controllers or
        /// API controllers (unless you want to change the defaults), as Unity
        /// allows resolving a concrete type even if it was not previously
        /// registered.
        /// </remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below.
            // Make sure to add a Unity.Configuration to the using statements.
            // container.LoadConfiguration();
            container.RegisterType<AccountController>(new InjectionConstructor());
            
            container.RegisterType<ManageController>(new InjectionConstructor());


            container.RegisterType<ISortingService, SortingService>();
            container.RegisterType<ISystemService<DrinkDto>, SystemService>();
            container.RegisterType<ISystemRepository<LocationFormDto>, LocationsRepository>();
            container.RegisterType<ISystemRepository<DrinkType>, DrinkTypeRepository>();
            container.RegisterType<ISystemRepository<DrinkDto>, SystemRepository>();
            container.RegisterType<IGeneralData, GeneralData>();
            container.RegisterType<ISendDataAsync, SendDataAsync>();
            container.RegisterType<ISystemRepository<LocationFeedbackDto>, FeedbackRepository>(); 
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            // TODO: Register your type's mappings here.
            // container.RegisterType<IProductRepository, ProductRepository>();
        }
    }
}