using System.Runtime.CompilerServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsCRUD.Data;
using ProductsCRUD.DTOs.Product;
using ProductsCRUD.Models;

namespace ProductsCRUD.Controllers
{
    [ApiController]
    [Route("api")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsDbContext _context;
        private readonly IMapper _mapper;

        public ProductsController(ProductsDbContext productsDbContext, IMapper mapper)
        {
            _context = productsDbContext;
            _mapper = mapper;
        }

        [HttpPost("ProductAdd")]
        public async Task<ActionResult<Product>> AddProduct(ProductDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ListProducts), new { id = entity.ProductId, dto });
        }

        [HttpGet("Products")]
        public async Task<ActionResult<IEnumerable<Product>>> ListProducts()
        {
            var products = await _context.Products.ToListAsync();
            var dotList = _mapper.Map<List<ProductDto>>(products);
            return Ok(dotList);
        }

         [HttpPut("ProductEdit/{productId}")]
        public async Task<IActionResult> EditProduct(int productId, ProductDto dto)
        {
            dto.ProductId = productId;
            var existingProduct = await _context.Products.FindAsync(productId);
            if (existingProduct == null)
                return NotFound($"Product with Id {productId} not found");

            // Map only the necessary fields from DTO to entity
            _mapper.Map(dto, existingProduct);
            await _context.SaveChangesAsync();
            return Ok(existingProduct);
        }

        // Delete a product
        [HttpDelete("ProductDelete/{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                return NotFound($"Product with Id {productId} not found");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // Get a product by ID
        [HttpGet("SelectProductById/{productId}")]
        public async Task<ActionResult<ProductDto>> SelectProductById(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                return NotFound($"Product with Id {productId} doesn't exist");

            var productDto = _mapper.Map<ProductDto>(product); // Map entity to DTO
            return Ok(productDto);
        }

        // List all products by category ID
        [HttpGet("ListProductsByCategory/{productCategoryId}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> ListProductsByCategory(int productCategoryId)
        {
            var products = await _context.Products
                .Where(p => p.ProductCategoryId == productCategoryId)
                .ToListAsync();

            if (products == null || !products.Any())
                return NotFound($"No products found for CategoryId {productCategoryId}");

            var productDtos = _mapper.Map<List<ProductDto>>(products);
            return Ok(productDtos);
        }
    }
}