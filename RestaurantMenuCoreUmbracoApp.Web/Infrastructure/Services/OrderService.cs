using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Constants;
using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Handlers;
using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Helpers;
using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Interfaces;
using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Extensions;

namespace RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IContentService _contentService;
        private readonly IUmbracoHelperService _umbracoHelperService;
        private readonly ApplicationSettings _applicationSettings;
        private static readonly int _userId = -1;
        public OrderService(IOptions<ApplicationSettings> applicationSettingsAccessor, IContentService contentService, IUmbracoHelperService umbracoHelperService)
        {
            _applicationSettings = applicationSettingsAccessor.Value;
            _contentService = contentService;
            _umbracoHelperService = umbracoHelperService;

        }
        public SubmitOrderResponseModel SubmitOrder(SubmitOrderRequestModel model)
        {
            var response = new SubmitOrderResponseModel();
            var parent = GetContentByHashId(model.ParentId);
            var table = GetContentByHashId(model.TableId);
            if (parent is not null && table is not null)
            {
                var order = CreateOrder(parent.Key, table.Key, model.OrderItems);
                if (order is not null)
                {
                    response.Created = true;
                    response.Published = PublishOrder(order, out string orderId);
                    response.Id = orderId;
                }
            }
            return response;
        }

        public IEnumerable<OrderModel> GetOrderList(string restaurantHashId, bool activeOrders)
        {
            var restaurantId = HashIdsHandler.HashIdToInt(restaurantHashId);
            var helper = _umbracoHelperService.GetHelper();
            var restaurant = helper.Content(restaurantId);
            if (restaurant is null || restaurant.ContentType.Alias is not Restaurant.ModelTypeAlias) return null;
            var orders = restaurant.Descendants<Order>().Where(
                order => activeOrders ? order.Status == OrderStatus.Submitted : order.Status != OrderStatus.Submitted).Select(order => new OrderModel(order));
            return orders;
        }

        public IContent GetOrder(string hashId)
        {
            var id = HashIdsHandler.HashIdToInt(hashId);
            var order = _contentService.GetById(id);
            return order;
        }

        public bool UpdateOrderStatus(string hashId, string status)
        {
            if (status != OrderStatus.Submitted && status != OrderStatus.Completed && status != OrderStatus.Cancelled) return false;
            var orderId = HashIdsHandler.HashIdToInt(hashId);
            var order = _contentService.GetById(orderId);
            if(order is null) return false; 
            var statusList = new List<string> { status };
            order.SetValue("status", JsonConvert.SerializeObject(statusList));
            var result = _contentService.SaveAndPublish(order);
            return result.Success;
        }

        private IContent CreateOrder(Guid parentGuid, Guid tableGuid, IEnumerable<RequestOrderItemModel> orderItems)
        {
            var now = DateTime.Now;
            var dateString = now.ToString("yyyy-MM-dd HH:mm:ss");
            var order = _contentService.Create(dateString, parentGuid, Order.ModelTypeAlias, _userId);
            var status = new List<string> { _applicationSettings.DefaultOrderStatus };
            order.SetValue("status", JsonConvert.SerializeObject(status));

            order.SetValue("orderTime", dateString);

            var tableUri = GuidToUri(tableGuid);
            order.SetValue("table", tableUri);

            if (orderItems.Any())
            {
                var nestedContent = new List<Dictionary<string, object>>();
                int index = -1;
                foreach (var item in orderItems)
                {
                    index++;
                    Guid itemKey = Guid.NewGuid();

                    var menuItem = GetContentByHashId(item.MenuItemId);
                    if (menuItem is not null)
                    {
                        var menuItemUri = GuidToUri(menuItem.Key);
                        nestedContent.Add(CreateNestedContentMenuItem(itemKey, index, item.Amount, menuItemUri));
                    }
                }
                order.SetValue("orderItems", JsonConvert.SerializeObject(nestedContent));
            }


            return order;
        }

        private Dictionary<string, object> CreateNestedContentMenuItem(Guid key, int index, int amount, Uri menuItemUri)
        {
            var menuItem = new Dictionary<string, object>(){
                { "key", key },
                { "name", string.Format("Order Item ({0})", index + 1) },
                { "ncContentTypeAlias", OrderItem.ModelTypeAlias },
                { "amount", amount },
                { "menuItem", menuItemUri.ToString() },
            };
            return menuItem;
        }

        private Uri GuidToUri(Guid guid, string entityType = "document")
        {
            GuidUdi udi = new GuidUdi(entityType, guid);
            var uri = udi.UriValue;
            return uri;
        }
        private bool PublishOrder(IContent order, out string orderId)
        {
            orderId = string.Empty;
            if (order is null) return false;
            var result = _contentService.SaveAndPublish(order);
            if (result.Success)
            {
                orderId = HashIdsHandler.IntToHashId(result.Content.Id);
            }
            return result.Success;
        }

        private IContent GetContentByHashId(string hashId)
        {
            var id = HashIdsHandler.HashIdToInt(hashId);
            var content = _contentService.GetById(id);
            return content;
        }
    }
}
