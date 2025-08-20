using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsCRUD.Data;
using ProductsCRUD.Models;

namespace ProductsCRUD.Controllers
{
    [ApiController]
    [Route("api")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsDbContext _context;

        public ProductsController(ProductsDbContext productsDbContext)
        {
            _context = productsDbContext;
        }

        [HttpPost("ProductAdd")]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ListProducts), new { id = product.ProductId, product });
        }

        [HttpGet("Products")]
        public async Task<ActionResult<IEnumerable<Product>>> ListProducts()
        {
            return await _context.Products.ToListAsync();
        }

        [HttpPut("ProductEdit/{productId}")]
        public async Task<IActionResult> EditProduct(int productId, Product product)
        {
            if (productId != product.ProductId)
                return BadRequest();

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("ProductDelete/{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("SelectProductById/{productId}")]
        public async Task<ActionResult<Product>> SelectProductById(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                return NotFound($"Product with Id {productId} doesn't exist");
            }

            return Ok(product);
        }


    }
}