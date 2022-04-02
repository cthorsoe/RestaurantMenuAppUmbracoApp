using System;

namespace RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Models
{
    public class SubmitOrderResponseModel
    {
        public bool Created { get; set; }
        public bool Published { get; set; }
        public string Id { get; set; }
    }
}
