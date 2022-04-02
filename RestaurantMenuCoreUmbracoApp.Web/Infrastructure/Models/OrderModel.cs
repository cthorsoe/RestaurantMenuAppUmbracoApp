using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Models
{
    public class OrderModel
    {
        public OrderModel(Order order)
        {
            Id = HashIdsHandler.IntToHashId(order.Id);
            Name = order.Name;
            Status = order.Status;
            Time = order.OrderTime;
            OrderItems = order.OrderItems.Select(item => new OrderItemModel()
            {
                ItemName = item.MenuItem.Name,
                Amount = item.Amount
            });
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public DateTime Time { get; set; }
        public IEnumerable<OrderItemModel> OrderItems { get; set; }
    }
}
