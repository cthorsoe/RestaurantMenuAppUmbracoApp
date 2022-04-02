using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Models;
using System;

namespace RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Helpers
{
    public class ApplicationSettingsHelper
    {
        static IServiceProvider services = null;
        /// <summary>
        /// Provides static access to the framework's services provider
        /// </summary>
        public static IServiceProvider Services
        {
            get { return services; }
            set
            {
                if (services != null)
                {
                    throw new Exception("Can't set once a value has already been set.");
                }
                services = value;
            }
        }

        /// <summary>
        /// Provides static access to the current HttpContext
        /// </summary>
        public static HttpContext HttpContext_Current
        {
            get
            {
                IHttpContextAccessor httpContextAccessor = services.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;
                return httpContextAccessor?.HttpContext;
            }
        }

        public static IHostEnvironment HostingEnvironment
        {
            get
            {
                return services.GetService(typeof(IHostEnvironment)) as IHostEnvironment;
            }
        }

        /// <summary>
        /// Configuration settings from appsetting.json.
        /// </summary>
        public static ApplicationSettings Config
        {
            get
            {
                //This works to get file changes.
                var s = services.GetService(typeof(IOptionsMonitor<ApplicationSettings>)) as IOptionsMonitor<ApplicationSettings>;
                ApplicationSettings config = s.CurrentValue;

                return config;
            }
        }
    }
}
