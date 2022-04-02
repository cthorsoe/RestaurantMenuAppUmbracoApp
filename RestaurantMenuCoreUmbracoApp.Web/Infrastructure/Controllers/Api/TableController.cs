using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Controllers;
using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Interfaces;

namespace RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Controllers.Api
{
    [Route("api/v1/table/")]
    public class TableController : UmbracoApiController
    {
        private readonly ITableService _tableService;
        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [Route("list/{restaurantHashId}")]
        public IActionResult GetTables(string restaurantHashId)
        {
            var tables = _tableService.GetTables(restaurantHashId);
            if (tables is null) return NotFound();
            return Ok(tables);
        }
    }
}
