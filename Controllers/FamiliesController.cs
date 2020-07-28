using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingWebsiteCore.DataAccess;
using WeddingWebsiteCore.Models;

namespace WeddingWebsiteCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FamiliesController : ControllerBase
    {
        private readonly WeddingContext _context;
        public FamiliesController(WeddingContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFamilies()
        {
            var families = await _context.Families.ToListAsync();

            return Ok(families);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFamily(int id)
        {
            var family = await _context.Families.FindAsync(id);

            if (family == null)
            {
                return NotFound();
            }

            return Ok(family);
        }

        [HttpPost]
        public async Task<IActionResult> PostNewFamily([FromBody]Family family)
        {
            var existing = await _context.Families.FindAsync(family.FamilyId);
            if (existing != null)
            {
                return BadRequest("An object already exists with that ID");
            }

            try
            {
                await _context.AddAsync(family);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction(nameof(PostNewFamily), family, family.FamilyId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFamily(int id, [FromBody]Family family)
        {
            var existing = await _context.Families.FindAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            try
            {
                _context.Entry(family).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFamily(int id)
        {
            var family = await _context.Families.FindAsync(id);
            if (family == null)
            {
                return NotFound();
            }

            try
            {
                _context.Remove(family);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }
    }
}
