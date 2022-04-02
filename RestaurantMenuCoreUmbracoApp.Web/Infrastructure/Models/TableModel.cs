using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Handlers;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Models
{
    public class TableModel
    {
        public TableModel(Table table)
        {
            Id = HashIdsHandler.IntToHashId(table.Id);
            Name = table.Name;
        }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
