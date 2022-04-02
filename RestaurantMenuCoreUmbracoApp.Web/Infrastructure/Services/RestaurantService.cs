using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Handlers;
using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Interfaces;
using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Models;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Extensions;

namespace RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IUmbracoHelperService _umbracoHelperService;

        public RestaurantService(IUmbracoHelperService umbracoHelperService)
        {
            _umbracoHelperService = umbracoHelperService;
        }
        public IEnumerable<RestaurantMenuModel> GetMenus(string hashId)
        {
            var helper = _umbracoHelperService.GetHelper();
            var id = HashIdsHandler.HashIdToInt(hashId);
            var node = helper.Content(id);
            if (node is null) return null;
            var menus = node.Descendants<Menu>().Where(menu => !menu.HideInMenuList);
            return menus.Select(
                child => new RestaurantMenuModel(child)
            );
        }
    }
}
