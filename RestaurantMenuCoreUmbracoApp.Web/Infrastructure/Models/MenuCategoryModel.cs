using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Handlers;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Models
{
    public class MenuCategoryModel
    {
        public MenuCategoryModel(IPublishedContent node)
        {
            if (node is not null && node.ContentType.Alias is MenuCategory.ModelTypeAlias)
            {
                Id = HashIdsHandler.IntToHashId(node.Id);
                Name = node.Name;
            }
        }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
