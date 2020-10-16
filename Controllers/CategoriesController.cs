using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingWebsiteCore.Contracts;
using WeddingWebsiteCore.DataAccess;
using WeddingWebsiteCore.Models;

namespace WeddingWebsiteCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly WeddingContext _context;

        public CategoriesController(WeddingContext context)
        {
            _context = context;
        }

        [HttpGet(RouteContracts.GetAll)]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _context.Categories.ToListAsync();

            return Ok(categories);
        }

        [HttpGet(RouteContracts.GetItem)]
        public async Task<IActionResult> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost(RouteContracts.PostItem)]
        public async Task<IActionResult> PostNewCategory([FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            var existing = await _context.Categories.FindAsync(category.CategoryId);
            if (existing != null)
            {
                return BadRequest(ErrorMessageContracts.IdConflict);
            }

            try
            {
                await _context.AddAsync(category);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction(nameof(PostNewCategory), category, category.CategoryId);
        }

        [HttpPut(RouteContracts.PutItem)]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            if (id != category.CategoryId)
            {
                return BadRequest(ErrorMessageContracts.MismatchedId);
            }

            var existing = await _context.Categories.FindAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            try
            {
                existing.Name = category.Name;
                existing.ParentId = category.ParentId;

                _context.Entry(existing).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        [HttpDelete(RouteContracts.DeleteItem)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            try
            {
                _context.Categories.Remove(category);

                var children = await _context.Categories
                    .Where(x => x.ParentId.Equals(id))
                    .ToListAsync();

                foreach (var child in children)
                {
                    child.ParentId = null;
                    _context.Entry(child).State = EntityState.Modified;
                }
                    
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }
    }
}