using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Models;
using System.Collections.Generic;

namespace RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Interfaces
{
    public interface IRestaurantService
    {
        public IEnumerable<RestaurantMenuModel> GetMenus(string hashId);
    }
}
