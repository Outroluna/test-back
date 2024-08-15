using test_back.Models;

namespace test_back.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> AddCategoryAsync(Category category);
        Task <Category> UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
    }
}
