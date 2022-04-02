using System;

namespace RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Models
{
    public class RequestOrderItemModel
    {
        public string MenuItemId { get; set; }
        public int Amount { get; set; }
    }
}
