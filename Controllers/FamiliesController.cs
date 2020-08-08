using System;
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
    public class FamiliesController : ControllerBase
    {
        private readonly WeddingContext _context;
        public FamiliesController(WeddingContext context)
        {
            _context = context;
        }

        [HttpGet(RouteContracts.GetAll)]
        public async Task<IActionResult> GetAllFamilies()
        {
            var families = await _context.Families
                .Include(family => family.Members)
                .Include(family => family.Address)
                .ToListAsync();

            return Ok(families);
        }

        [HttpGet(RouteContracts.GetItem)]
        public async Task<IActionResult> GetFamily(int id)
        {
            var family = await _context.Families.FindAsync(id);

            if (family == null)
            {
                return NotFound();
            }

            return Ok(family);
        }

        [HttpPost(RouteContracts.PostItem)]
        public async Task<IActionResult> PostNewFamily([FromBody]Family family)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            var existing = await _context.Families.FindAsync(family.FamilyId);
            if (existing != null)
            {
                return BadRequest(ErrorMessageContracts.IdConflict);
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

        [HttpPut(RouteContracts.PutItem)]
        public async Task<IActionResult> UpdateFamily(int id, [FromBody]Family family)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            if (id != family.FamilyId)
            {
                return BadRequest(ErrorMessageContracts.MismatchedId);
            }

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

        [HttpDelete(RouteContracts.DeleteItem)]
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
