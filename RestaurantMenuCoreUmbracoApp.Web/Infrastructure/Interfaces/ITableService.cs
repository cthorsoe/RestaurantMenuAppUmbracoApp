using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Models;
using System.Collections.Generic;

namespace RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Interfaces
{
    public interface ITableService
    {
        public IEnumerable<TableModel> GetTables(string restaurantHashId);
    }
}
