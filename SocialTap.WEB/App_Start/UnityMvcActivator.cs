using System.Linq;
using System.Web.Mvc;
using System.Web.Http;
using Unity.AspNet.Mvc;
using Unity.WebApi;
[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SocialTap.WEB.UnityMvcActivator), nameof(SocialTap.WEB.UnityMvcActivator.Start))]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(SocialTap.WEB.UnityMvcActivator), nameof(SocialTap.WEB.UnityMvcActivator.Shutdown))]

namespace SocialTap.WEB
{
    /// <summary>
    /// Provides the bootstrapping for integrating Unity with ASP.NET MVC.
    /// </summary>
    public static class UnityMvcActivator
    {
        /// <summary>
        /// Integrates Unity when the application starts.
        /// </summary>
        public static void Start() 
        {
            FilterProviders.Providers.Remove(FilterProviders.Providers.OfType<FilterAttributeFilterProvider>().First());
            FilterProviders.Providers.Add(new UnityFilterAttributeFilterProvider(UnityConfig.Container));

            DependencyResolver.SetResolver(new Unity.AspNet.Mvc.UnityDependencyResolver(UnityConfig.Container));

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(UnityConfig.Container);

            // TODO: Uncomment if you want to use PerRequestLifetimeManager
            // Microsoft.Web.Infrastructure.DynamicModuleHelper.DynamicModuleUtility.RegisterModule(typeof(UnityPerRequestHttpModule));
        }

        /// <summary>
        /// Disposes the Unity container when the application is shut down.
        /// </summary>
        public static void Shutdown()
        {
            UnityConfig.Container.Dispose();
        }
    }
}