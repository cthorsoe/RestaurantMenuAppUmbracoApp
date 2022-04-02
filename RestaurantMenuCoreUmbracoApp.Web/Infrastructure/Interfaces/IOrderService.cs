using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Models;
using System.Collections.Generic;
using Umbraco.Cms.Core.Models;

namespace RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Interfaces
{
    public interface IOrderService
    {
        public SubmitOrderResponseModel SubmitOrder(SubmitOrderRequestModel model);
        public IEnumerable<OrderModel> GetOrderList(string restaurantHashId, bool activeOrders);
        public bool UpdateOrderStatus(string hashId, string status);
        public IContent GetOrder(string hashId);
    }
}
