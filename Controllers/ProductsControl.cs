using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test_back.Data;
using test_back.Models;
using test_back.Services;

namespace test_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsControl : ControllerBase
    {
        private readonly IProductService ProductService;
        public ProductsControl(IProductService productService)
        {
            ProductService = productService; ///создается экземпляр ProductService и внедряется его в контроллер
        }

        [HttpGet] ///указывает, что этот метод обрабатывает HTTP GET запросы
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts() ///возвращает объект ActionResult, который содержит результат выполнения метода
        {
            var products = await ProductService.GetAllProductsAsync(); ///вызывается метод в сервисе 
            ///возвращает результат вызова в переменную products
            return Ok(products); //Метод Ok создаёт объект OkObjectResult,
                                 //который возвращает HTTP-статус 200 (OK)
                                 //и передаёт коллекцию продуктов в теле ответа???
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await ProductService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost] ///Создание нового продукта
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            await ProductService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product); ///??????
        }
        [HttpPut("{id}")] ///Обновляет существующий продукт
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            await ProductService.UpdateProductAsync(product);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await ProductService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            await ProductService.DeleteProductAsync(id);
            return NoContent();
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        //{
        //    return await Context.Products.ToListAsync();
        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Product>> GetProduct(int id)
        //{
        //    var product = await Context.Products.FindAsync(id);

        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    return product;
        //}
    }
}
