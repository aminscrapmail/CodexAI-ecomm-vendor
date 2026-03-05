using EcommVendor.Api.Data;
using EcommVendor.Api.DTOs;
using EcommVendor.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcommVendor.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(AppDbContext dbContext) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll([FromQuery] string? search, [FromQuery] string? category)
    {
        var query = dbContext.Products.Where(p => !p.IsDeleted).AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p => p.Name.Contains(search) || p.Description.Contains(search));
        }

        if (!string.IsNullOrWhiteSpace(category))
        {
            query = query.Where(p => p.Category == category);
        }

        var products = await query.OrderByDescending(p => p.LastUpdated).ToListAsync();
        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> Create(ProductRequest request)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Category = request.Category,
            Description = request.Description,
            Stock = request.Stock,
            Price = request.Price,
            LastUpdated = DateTime.UtcNow,
            IsDeleted = false,
            ModifiedBy = request.ModifiedBy
        };

        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new { id = product.Id }, product);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Product>> Update(Guid id, ProductRequest request)
    {
        var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        if (product is null)
        {
            return NotFound();
        }

        product.Name = request.Name;
        product.Category = request.Category;
        product.Description = request.Description;
        product.Stock = request.Stock;
        product.Price = request.Price;
        product.ModifiedBy = request.ModifiedBy;
        product.LastUpdated = DateTime.UtcNow;

        await dbContext.SaveChangesAsync();
        return Ok(product);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, [FromQuery] string modifiedBy = "vendor-user")
    {
        var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        if (product is null)
        {
            return NotFound();
        }

        product.IsDeleted = true;
        product.ModifiedBy = modifiedBy;
        product.LastUpdated = DateTime.UtcNow;

        await dbContext.SaveChangesAsync();
        return NoContent();
    }
}
