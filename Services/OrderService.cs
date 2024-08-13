using Microsoft.EntityFrameworkCore;
using test_back.Data;
using test_back.Models;

namespace test_back.Services
{
    public class OrderService: IOrderService
    {
        private readonly ApplicationDbContext Context;

        public OrderService(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            ///  метод Include используется для загрузки связанных данных
            ///  ThenInclude() - вроде понятно но еще не очень (для загрузки связанных данных второго уровня)
            return await Context.Orders
                .Include(o => o.User)                   
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await Context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task AddOrderAsync(Order order)
        {
            Context.Orders.Add(order);
            await Context.SaveChangesAsync(); ///SaveChangesAsync генерирует SQL-запрос для обновления соответствующей записи в базе данных
        }

        public async Task UpdateOrderAsync(Order order)
        {
            Context.Entry(order).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await Context.Orders.FindAsync(id);
            if (order != null)
            {
                Context.Orders.Remove(order);
                await Context.SaveChangesAsync();
            }
        }
    }

}
