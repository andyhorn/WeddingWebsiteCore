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
    public class RegistryIconsController : ControllerBase
    {
        private readonly WeddingContext _context;

        public RegistryIconsController(WeddingContext context)
        {
            _context = context;
        }

        [HttpGet(RouteContracts.GetAll)]
        public async Task<IActionResult> GetAllRegistryIcons()
        {
            var icons = await _context.RegistryIcons.ToListAsync();

            return Ok(icons);
        }

        [HttpGet(RouteContracts.GetItem)]
        public async Task<IActionResult> GetRegistryIcon(int id)
        {
            var icon = await _context.RegistryIcons.FindAsync(id);

            if (icon == null)
            {
                return NotFound();
            }

            return Ok(icon);
        }

        [HttpPost(RouteContracts.PostItem)]
        public async Task<IActionResult> PostNewRegistryIcon([FromBody] RegistryIcon icon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            var existingIcon = await _context.RegistryIcons.FindAsync(icon.Id);
            if (existingIcon != null)
            {
                return BadRequest(ErrorMessageContracts.IdConflict);
            }

            try
            {
                await _context.AddAsync(icon);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction(nameof(PostNewRegistryIcon), icon, icon.Id);
        }

        [HttpPut(RouteContracts.PutItem)]
        public async Task<IActionResult> UpdateRegistryIcon(int id, [FromBody]RegistryIcon icon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            if (id != icon.Id)
            {
                return BadRequest(ErrorMessageContracts.MismatchedId);
            }

            var existingIcon = await _context.RegistryIcons.FindAsync(id);
            if (existingIcon == null)
            {
                return NotFound();
            }

            try
            {
                _context.Entry(existingIcon).CurrentValues.SetValues(icon);
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
        public async Task<IActionResult> DeleteRegistryIcon(int id)
        {
            var icon = await _context.RegistryIcons.FindAsync(id);

            if (icon == null)
            {
                return NotFound();
            }

            try
            {
                _context.Remove(icon);
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