using Microsoft.AspNetCore.Mvc;
using WebAPIv1.Domain.Contracts.Orders;
using WebAPIv1.Domain.Models;
using WebAPIv1.Services.Orders;

namespace WebAPIv1.Controllers.OrderController
{
    [ApiController]
    [Route("[controller]")]

    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost()]
        public IActionResult CreateOrder(CreateOrderRequest request)
        {
            //Map Req to Model
            Order newOrder = new Order(
                Guid.NewGuid(),
                request.PetId,
                request.OrderStatus,
                request.Price,
                request.Complete
                );

            _orderService.CreateOrder(newOrder);

            OrderResponse response = new OrderResponse(
                newOrder.OrderId,
                newOrder.PetId,
                newOrder.OrderStatus,
                newOrder.Price,
                newOrder.Complete == 1 ? true : false
                );

            return CreatedAtAction(
                actionName: nameof(GetOrder),
                routeValues: new { id = newOrder.OrderId },
                value: response);
        }

        [HttpGet(Name = "/Order/{id:guid}")]
        public async Task<IActionResult> GetOrder(Guid id)
        {
            Order orderItem = await _orderService.GetOrderById(id);
            if(orderItem is not null)
            {
                OrderResponse response = new OrderResponse(
                    orderItem.OrderId,
                    orderItem.PetId,
                    orderItem.OrderStatus,
                    orderItem.Price,
                    orderItem.Complete == 1 ? true : false);

                return Ok(response);
            }

            return NotFound();
        }        
        [HttpGet("~/Orders")]
        public async Task<IActionResult> GetOrders()
        {
            List<Order> allOrders = await _orderService.GetAllOrders();
            return Ok(allOrders);
        }

        [HttpDelete(Name = "/Order/{id:guid}")]
        public IActionResult DeleteOrder(Guid id)
        {
            _orderService.DeleteOrder(id);
            return NoContent();
        }
    }
}
