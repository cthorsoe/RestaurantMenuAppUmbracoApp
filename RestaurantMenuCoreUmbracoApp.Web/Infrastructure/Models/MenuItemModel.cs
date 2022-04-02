using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Handlers;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Models
{
    public class MenuItemModel
    {
        public MenuItemModel(MenuItem menuItem)
        {
            if (menuItem is not null)
            {
                Id = HashIdsHandler.IntToHashId(menuItem.Id);
                Name = menuItem.Name;
                Description = menuItem.Description;
                Price = menuItem.Price;
            }
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}
