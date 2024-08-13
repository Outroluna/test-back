using Microsoft.AspNetCore.Mvc;
using test_back.Models;
using test_back.Services;

namespace test_back.Controllers
{
    [Route("api/[Orders]")]
    [ApiController]
    public class OrdersControl: ControllerBase
    {
        private readonly IOrderService OrderService;

        public OrdersControl(IOrderService orderService)
        {
            OrderService = orderService;
        }

        ///указывает, что этот метод обрабатывает HTTP GET запросы
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            var orders = await OrderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await OrderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            await OrderService.AddOrderAsync(order);
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            await OrderService.UpdateOrderAsync(order);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await OrderService.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}
