using Microsoft.AspNetCore.Mvc;
using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Constants;
using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Interfaces;
using RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Models;
using Umbraco.Cms.Web.Common.Controllers;

namespace RestaurantMenuCoreUmbracoApp.Web.Infrastructure.Controllers.Api
{
    [Route("api/v1/order/")]
    public class OrderController : UmbracoApiController
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("submit")]
        public IActionResult SubmitOrder(SubmitOrderRequestModel model)
        {
            var response = _orderService.SubmitOrder(model);
            if (response.Created is false) return NotFound();
            if (response.Published is false) return Problem();
            return Ok(response.Id);
        }

        [Route("list/{restaurantHashId}/{activeOrders:bool?}")]
        public IActionResult GetOrders(string restaurantHashId, bool activeOrders = true)
        {
            var orders = _orderService.GetOrderList(restaurantHashId, activeOrders);
            return Ok(orders);
        }

        [HttpPost("cancel/{orderHashId}")]
        public IActionResult CancelOrder(string orderHashId)
        {
            var updatedOrder = _orderService.UpdateOrderStatus(orderHashId, OrderStatus.Cancelled);
            if (updatedOrder is false) return NotFound();
            return Ok();
        }

        [HttpPost("complete/{orderHashId}")]
        public IActionResult CompleteOrder(string orderHashId)
        {
            var updatedOrder = _orderService.UpdateOrderStatus(orderHashId, OrderStatus.Completed);
            if (updatedOrder is false) return NotFound();
            return Ok();
        }

        [HttpPost("resubmit/{orderHashId}")]
        public IActionResult ResubmitOrder(string orderHashId)
        {
            var updatedOrder = _orderService.UpdateOrderStatus(orderHashId, OrderStatus.Submitted);
            if (updatedOrder is false) return NotFound();
            return Ok();
        }

        [Route("get/{id}")]
        public IActionResult Test(string id)
        {
            var order = _orderService.GetOrder(id);
            return Ok(order);
        }
    }
}
