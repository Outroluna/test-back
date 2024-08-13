using Microsoft.AspNetCore.Mvc;
using test_back.Models;
using test_back.Services;

namespace test_back.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesControl : ControllerBase
    {
        private readonly ICategoryService CategoryService;

        public CategoriesControl(ICategoryService categoryService)
        {
            CategoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await CategoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await CategoryService.GetCategoryByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> CreateCategory(Category category)
        {
            await CategoryService.CreateCategoryAsync(category);
            return CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            await CategoryService.UpdateCategoryAsync(category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await CategoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await CategoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
    }
}

