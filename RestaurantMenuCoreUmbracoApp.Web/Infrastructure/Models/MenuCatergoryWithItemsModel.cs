using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Handlers;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Extensions;

namespace RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Models
{
    public class MenuCategoryWithItemsModel : MenuCategoryModel
    {
        public MenuCategoryWithItemsModel(IPublishedContent node) : base(node)
        {
            if (node is not null && node.ContentType.Alias is MenuCategory.ModelTypeAlias)
            {
                Id = HashIdsHandler.IntToHashId(node.Id);
                Name = node.Name;
                Items = node.Children<MenuItem>().Select(item => new MenuItemModel(item));
            }
        }

        public IEnumerable<MenuItemModel> Items { get; set; }
    }
}
