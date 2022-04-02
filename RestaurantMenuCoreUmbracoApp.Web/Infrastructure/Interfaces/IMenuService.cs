using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Models;
using System.Collections.Generic;

namespace RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Interfaces
{
    public interface IMenuService
    {
        public IEnumerable<T> GetCategories<T>(string hashId);
    }
}
