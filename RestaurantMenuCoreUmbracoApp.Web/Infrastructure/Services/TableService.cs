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
    public class TableService : ITableService
    {
        private readonly IUmbracoHelperService _umbracoHelperService;

        public TableService(IUmbracoHelperService umbracoHelperService)
        {
            _umbracoHelperService = umbracoHelperService;
        }
        public IEnumerable<TableModel> GetTables(string restaurantHashId)
        {
            var restaurantId = HashIdsHandler.HashIdToInt(restaurantHashId);
            var helper = _umbracoHelperService.GetHelper();
            var restaurant = helper.Content(restaurantId);
            if (restaurant is null || restaurant.ContentType.Alias is not Restaurant.ModelTypeAlias) return null;
            var tables = restaurant.Descendants<Table>().Select(table => new TableModel(table));
            return tables;
        }
    }
}
