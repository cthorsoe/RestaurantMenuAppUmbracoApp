using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Controllers;
using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Models;
using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Interfaces;

namespace RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Controllers.Api
{
    [Route("api/v1/menu/")]
    public class MenusController : UmbracoApiController
    {
        private readonly IMenuService _menuService;
        public MenusController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [Route("categories/{hashId}/{getItems:bool?}")]
        public IActionResult GetCategories(string hashId, bool getItems)
        {
            var categories = getItems ? _menuService.GetCategories<MenuCategoryWithItemsModel>(hashId) : _menuService.GetCategories<MenuCategoryModel>(hashId);
            if (categories is null) return NotFound();
            return Ok(categories);
        }

    }
}
