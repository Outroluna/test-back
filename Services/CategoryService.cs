using Microsoft.EntityFrameworkCore;
using test_back.Data;
using test_back.Models;

namespace test_back.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext Context;

        public CategoryService(ApplicationDbContext context)
        {
            Context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await Context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await Context.Categories.FindAsync(id);
        }

        public async Task CreateCategoryAsync(Category category)
        {
            Context.Categories.Add(category);
            await Context.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            Context.Entry(category).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await Context.Categories.FindAsync(id);
            if (category != null)
            {
                Context.Categories.Remove(category);
                await Context.SaveChangesAsync();
            }
        }

    }
}
