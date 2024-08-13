using Microsoft.EntityFrameworkCore;
using test_back.Data;
using test_back.Models;

namespace test_back.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext Context;

        public ProductService(ApplicationDbContext context)
        {
            Context = context;
        }

        
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await Context.Products.ToListAsync(); ///Метод возваращает список всех products
        }
        public async Task AddProductAsync(Product product)
        {
            Context.Products.Add(product);
            await Context.SaveChangesAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await Context.Products.FindAsync(id);
        }

        

        public async Task UpdateProductAsync(Product product)
        {
            Context.Entry(product).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await Context.Products.FindAsync(id);
            if (product != null)
            {
                Context.Products.Remove(product);
                await Context.SaveChangesAsync();
            }
        }
    }
}