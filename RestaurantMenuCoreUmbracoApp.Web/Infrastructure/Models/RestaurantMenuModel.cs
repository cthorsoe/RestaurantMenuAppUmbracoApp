using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Handlers;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Models
{
    public class RestaurantMenuModel
    {
        public RestaurantMenuModel(Menu menu)
        {
            if(menu is not null)
            {
                Id = HashIdsHandler.IntToHashId(menu.Id);
                Name = menu.Name;
            }
        }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
