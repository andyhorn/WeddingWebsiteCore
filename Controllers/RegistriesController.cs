using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using WeddingWebsiteCore.Contracts;
using WeddingWebsiteCore.DataAccess;

namespace WeddingWebsiteCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistriesController : ControllerBase
    {
        private readonly WeddingContext _context;
        public RegistriesController(WeddingContext context)
        {
            _context = context;
        }

        [HttpGet(RouteContracts.GetAll)]
        public async Task<IActionResult> GetAllRegistries()
        {
            var registries = await _context.Registries.ToListAsync();

            return Ok(registries);
        }

        [HttpGet(RouteContracts.GetItem)]
        public async Task<IActionResult> GetRegistry(int id)
        {
            var registry = await _context.Registries.FindAsync(id);

            if (registry == null)
            {
                return NotFound();
            }

            return Ok(registry);
        }

        [HttpPost(RouteContracts.PostItem)]
        public async Task<IActionResult> PostNewRegistry([FromBody]Models.Registry registry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            var exists = await ExistsAsync(registry.RegistryId);
            if (exists)
            {
                return BadRequest(ErrorMessageContracts.IdConflict);
            }

            try
            {
                await _context.AddAsync(registry);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction(nameof(PostNewRegistry), registry.RegistryId);
        }

        [HttpPut(RouteContracts.PutItem)]
        public async Task<IActionResult> UpdateRegistry(int id, [FromBody]Models.Registry registry)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            var exists = await ExistsAsync(registry.RegistryId);
            if (!exists)
            {
                return NotFound();
            }

            if (id != registry.RegistryId)
            {
                return BadRequest(ErrorMessageContracts.MismatchedId);
            }

            try
            {
                _context.Entry(registry).State = EntityState.Modified;
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
        public async Task<IActionResult> DeleteRegistry(int id)
        {
            var exists = await ExistsAsync(id);
            if (!exists)
            {
                return NotFound();
            }

            var item = await _context.Registries.FindAsync(id);

            try
            {
                _context.Remove(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        private async Task<bool> ExistsAsync(int id)
        {
            var item = await _context.Registries.FindAsync(id);
            return item != null;
        }
    }
}
