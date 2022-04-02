using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Handlers;
using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Interfaces;
using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Extensions;

namespace RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Services
{
    public class MenuService : IMenuService
    {
        private readonly IUmbracoHelperService _umbracoHelperService;
        private readonly IHashIdsService _hashIdsService;

        public MenuService(IUmbracoHelperService umbracoHelperService, IHashIdsService hashIdsService)
        {
            _umbracoHelperService = umbracoHelperService;
            _hashIdsService = hashIdsService;
        }

        /// <summary>
        /// Method to get menu categories. 
        /// Pass Umbraco Node Id of Document Type "Menu". 
        /// Valid object types are MenuCategoryModel or MenuCategoryWithItemsModel
        /// Pass the latter to get MenuItems in dataset and the former if you don't want the items.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<T> GetCategories<T>(string hashId)
        {
            if (typeof(T) != typeof(MenuCategoryModel) && typeof(T) != typeof(MenuCategoryWithItemsModel))
            {
                return null;
            }
            var helper = _umbracoHelperService.GetHelper();
            var id = HashIdsHandler.HashIdToInt(hashId);
            var menu = helper.Content(id) as Menu;
            if (menu is null) return null;
            IEnumerable<T> categories;
            IEnumerable<T> inheritedCategories;
            if (typeof(T) == typeof(MenuCategoryWithItemsModel))
            {
                categories = (IEnumerable<T>)menu.Children.Select(
                    cat => new MenuCategoryWithItemsModel(cat)
                );
                inheritedCategories = (IEnumerable<T>)menu.InheritedCategories.Select(
                    cat => new MenuCategoryWithItemsModel(cat)
                );
            }
            else
            {
                categories = (IEnumerable<T>)menu.Children.Select(
                    cat => new MenuCategoryModel(cat)
                );
                inheritedCategories = (IEnumerable<T>)menu.InheritedCategories.Select(
                    cat => new MenuCategoryModel(cat)
                );
            }
            categories = categories.Concat(inheritedCategories);
            return categories;
        }
    }
}
