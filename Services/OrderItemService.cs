using Microsoft.EntityFrameworkCore;
using test_back.Data;
using test_back.Models;

namespace test_back.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly ApplicationDbContext Context;

        public OrderItemService(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<IEnumerable<OrderItem>> GetAllOrderItemsAsync()
        {
            return await Context.OrderItems
                .Include(oi => oi.Product)
                .ToListAsync();
        }

        public async Task<OrderItem> GetOrderItemByIdAsync(int id)
        {
            return await Context.OrderItems
                .Include(oi => oi.Product)
                .FirstOrDefaultAsync(oi => oi.Id == id);
        }

        public async Task CreateOrderItemAsync(OrderItem orderItem)
        {
            Context.OrderItems.Add(orderItem);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateOrderItemAsync(OrderItem orderItem)
        {
            Context.Entry(orderItem).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task DeleteOrderItemAsync(int id)
        {
            var orderItem = await Context.OrderItems.FindAsync(id);
            if (orderItem != null)
            {
                Context.OrderItems.Remove(orderItem);
                await Context.SaveChangesAsync();
            }
        }
    }
}
