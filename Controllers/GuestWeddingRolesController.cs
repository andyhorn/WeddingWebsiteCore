using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeddingWebsiteCore.Contracts;
using WeddingWebsiteCore.DataAccess;
using WeddingWebsiteCore.Models;

namespace WeddingWebsiteCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestWeddingRolesController : ControllerBase
    {
        private WeddingContext _context { get; set; }

        public GuestWeddingRolesController(WeddingContext context)
        {
            _context = context;
        }

        [HttpPost(RouteContracts.PostItem)]
        public async Task<IActionResult> PostNewGuestWeddingRole([FromBody] GuestWeddingRole guestWeddingRole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            if (guestWeddingRole.GuestWeddingRoleId != 0)
            {
                return BadRequest(ErrorMessageContracts.IdConflict);
            }

            try
            {
                _context.Add(guestWeddingRole);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction(nameof(PostNewGuestWeddingRole), guestWeddingRole, guestWeddingRole.GuestWeddingRoleId);
        }

        [HttpDelete(RouteContracts.DeleteItem)]
        public async Task<IActionResult> DeleteGuestWeddingRole(int id)
        {
            var existing = await _context.GuestWeddingRoles.FindAsync(id);

            if (existing == null)
            {
                return NotFound();
            }

            try
            {
                _context.GuestWeddingRoles.Remove(existing);
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