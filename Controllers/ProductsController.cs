using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Optimization_Project.DTO;
using ProductsApi.Data;
using ProductsApi.DTO;
using ProductsApi.Models;

namespace ProductsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
      private readonly AppDbContext context;
        public ProductsController(AppDbContext context)
        {
           this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto >>> GetProducts(int page = 1, int pageSize = 10)
        {
            if (page < 1 || pageSize < 1 || pageSize > 100)
               { return BadRequest("Invalid pagination parameters."); }

            var products = await context.Products
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    CategoryName = p.Category != null
                        ? p.Category.Name
                        : null
                })
                .ToListAsync();

            return Ok(products);
        }


        [HttpPut("{id}")]
         public async Task<IActionResult> UpdateProduct(int id,  UpdateProductDto dto)
        {
            var product = await context.Products
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound($"Product with ID {id} not found.");

            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Product name is required.");

            if (dto.Price < 0)
                return BadRequest("Price cannot be negative.");

            var categoryExists = await context.Categories
                .AnyAsync(c => c.Id == dto.CategoryId);

            if (!categoryExists)
                return BadRequest("Invalid CategoryId.");

            product.Name = dto.Name;
            product.Price = dto.Price;
            product.CategoryId = dto.CategoryId;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(
                    "The product was modified by another user. Please reload and try again.");
            }

            return NoContent();
        }
    }

    
}
