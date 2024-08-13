using Microsoft.AspNetCore.Mvc;
using test_back.Models;
using test_back.Services;

namespace test_back.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemsControl : ControllerBase
    {
        private readonly IOrderItemService OrderItemService;

        public OrderItemsControl(IOrderItemService orderItemService)
        {
            OrderItemService = orderItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItems()
        {
            var orderItems = await OrderItemService.GetAllOrderItemsAsync();
            return Ok(orderItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderItem>> GetOrderItem(int id)
        {
            var orderItem = await OrderItemService.GetOrderItemByIdAsync(id);

            if (orderItem == null)
            {
                return NotFound();
            }

            return Ok(orderItem);
        }

        [HttpPost]
        public async Task<ActionResult<OrderItem>> CreateOrderItem(OrderItem orderItem)
        {
            await OrderItemService.CreateOrderItemAsync(orderItem);
            return CreatedAtAction(nameof(GetOrderItem), new { id = orderItem.Id }, orderItem);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrderItem(int id, OrderItem orderItem)
        {
            if (id != orderItem.Id)
            {
                return BadRequest();
            }

            await OrderItemService.UpdateOrderItemAsync(orderItem);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var orderItem = await OrderItemService.GetOrderItemByIdAsync(id);
            if (orderItem == null)
            {
                return NotFound();
            }

            await OrderItemService.DeleteOrderItemAsync(id);
            return NoContent();
        }
    }
}
