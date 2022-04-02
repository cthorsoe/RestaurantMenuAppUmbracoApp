using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Controllers;
using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Interfaces;

namespace RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Controllers.Api
{
    [Route("api/v1/restaurant/")]
    public class RestaurantController : UmbracoApiController
    {
        private readonly IRestaurantService _restaurantService;
        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [Route("menus/{hashId}")]
        public IActionResult GetMenus(string hashId)
        {
            var menus = _restaurantService.GetMenus(hashId);
            if (menus is null) return NotFound();
            return Ok(menus);
        }
    }
}
