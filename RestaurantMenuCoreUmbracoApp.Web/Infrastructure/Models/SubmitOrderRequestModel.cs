using System;
using System.Collections.Generic;

namespace RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Models
{
    public class SubmitOrderRequestModel
    {
        public string ParentId { get; set; }
        public string TableId { get; set; }
        public IEnumerable<RequestOrderItemModel> OrderItems { get; set; }
    }
}
