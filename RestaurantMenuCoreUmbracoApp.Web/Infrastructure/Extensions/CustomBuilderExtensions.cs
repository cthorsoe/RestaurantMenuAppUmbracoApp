using HashidsNet;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Interfaces;
using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Models;
using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Services;
using Umbraco.Cms.Core.DependencyInjection;

namespace RestaurantMenuCoreUmbracoApp.Web.Extensions
{
    public static class MyCustomBuilderExtensions
    {
        private static readonly string _hashIdsSalt = "f9c33c81-7c4f-4c04-99d4-c7bb8ecfa534";
        private static IUmbracoBuilder RegisterCustomServices(this IUmbracoBuilder builder)
        {
            builder.Services.AddSingleton<IUmbracoHelperService, UmbracoHelperService>();
            builder.Services.AddSingleton<IRestaurantService, RestaurantService>();
            builder.Services.AddSingleton<IMenuService, MenuService>();
            builder.Services.AddSingleton<IOrderService, OrderService>();
            builder.Services.AddSingleton<IHashIdsService, HashIdsService>();
            builder.Services.AddSingleton<ITableService, TableService>();
            return builder;
        }

        private static IUmbracoBuilder RegisterIHashIdsService(this IUmbracoBuilder builder)
        {
            builder.Services.AddSingleton<IHashids>(_ => new Hashids(_hashIdsSalt, 10));
            return builder;
        }
        private static IUmbracoBuilder RegisterApplicationSettings(this IUmbracoBuilder builder)
        {
            var appSection =
                builder.Config.GetSection("Application");
            builder.Services.Configure<ApplicationSettings>(appSection);

            return builder;
        }

        public static IUmbracoBuilder AddCustomServices(this IUmbracoBuilder builder)
        {
            builder.RegisterCustomServices();
            builder.RegisterIHashIdsService();
            builder.RegisterApplicationSettings();
            return builder;
        }
    }
}