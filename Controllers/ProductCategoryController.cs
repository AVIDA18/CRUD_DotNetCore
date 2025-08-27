using System.Runtime.CompilerServices;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductsCRUD.Data;
using ProductsCRUD.DTOs.ProductCategory;
using ProductsCRUD.Models;

namespace ProductsCRUD.Controllers
{
    [ApiController]
    [Route("api")]
    public class PorductCategoryController : ControllerBase
    {
        private readonly ProductsDbContext _context;
        private readonly IMapper _mapper;

        public PorductCategoryController(ProductsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost("ProductCategoryAdd")]
        public async Task<ActionResult<ProductCategory>> AddProductCategory(ProductCategoryDto dto)
        {
            var entity = _mapper.Map<ProductCategory>(dto);

            _context.ProductCategories.Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ListProductCategories), new { id = entity.ProductCategoryId, dto });
        }

        [HttpGet("ProductCategory")]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> ListProductCategories()
        {
            var categories = await _context.ProductCategories.ToListAsync();
            var dtoList = _mapper.Map<List<ProductCategoryDto>>(categories);
            return Ok(dtoList);
        }

        [HttpPut("ProductCategoryEdit/{productCategoryId}")]
        public async Task<IActionResult> EditProductCategory(int productCategoryId, ProductCategoryDto dto)
        {
            dto.ProductCategoryId = productCategoryId;
            var existingEntry = await _context.ProductCategories.FindAsync(productCategoryId);
            if (existingEntry == null)
                return NotFound($"Product Category with Id {productCategoryId} not found");

            _mapper.Map(dto, existingEntry);
            await _context.SaveChangesAsync();
            return Ok(_mapper.Map<ProductCategoryDto>(existingEntry));
            // _context.Entry(productCategory).State = EntityState.Modified;
            // await _context.SaveChangesAsync();
            // return Ok();
        }

        [HttpDelete("ProductCategoryDelete/{productCategoryId}")]
        public async Task<IActionResult> DeleteProductCategory(int productCategoryId)
        {
            var productCategory = await _context.ProductCategories.FindAsync(productCategoryId);
            if (productCategory == null)
            {
                return NotFound();
            }

            _context.ProductCategories.Remove(productCategory);
            await _context.SaveChangesAsync();
            return Ok();
        }

        
    }
}