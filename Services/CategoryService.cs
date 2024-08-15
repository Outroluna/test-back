using System.Collections.Generic;
using System.Threading.Tasks;
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
            return await Context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            Context.Categories.Add(category);
            await Context.SaveChangesAsync();
            return category;
        }
        public async Task<Category> UpdateCategoryAsync(Category category)
        {
            Context.Categories.Update(category);
            await Context.SaveChangesAsync();
            return category;
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await Context.Categories.FindAsync(id);
            if (category != null)
            {
                Context.Categories.Remove(category);
                await Context.SaveChangesAsync();
            }
        } ///if == nul -> false 

    }
}
