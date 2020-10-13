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
    public class RsvpsController : ControllerBase
    {
        private readonly WeddingContext _context;
        public RsvpsController(WeddingContext context)
        {
            _context = context;
        }

        [HttpGet(RouteContracts.GetAll)]
        public async Task<IActionResult> GetAllRsvps()
        {
            var rsvps = await _context.Rsvps.ToListAsync();

            return Ok(rsvps);
        }

        [HttpGet(RouteContracts.GetItem)]
        public async Task<IActionResult> GetRsvp(int id)
        {
            var rsvp = await _context.Rsvps.FindAsync(id);

            if (rsvp == null)
            {
                return NotFound();
            }

            return Ok(rsvp);
        }

        [HttpPost(RouteContracts.PostItem)]
        public async Task<IActionResult> PostNewRsvp([FromBody]Rsvp rsvp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            var exists = await ExistsAsync(rsvp.RsvpId);
            if (exists)
            {
                return BadRequest(ErrorMessageContracts.IdConflict);
            }

            try
            {
                await _context.AddAsync(rsvp);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction(nameof(PostNewRsvp), rsvp.RsvpId);
        }

        [HttpPut(RouteContracts.PutItem)]
        public async Task<IActionResult> UpdateRsvp(int id, [FromBody]Rsvp rsvp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            var exists = await ExistsAsync(id);
            if (exists)
            {
                return BadRequest(ErrorMessageContracts.IdConflict);
            }

            if (id != rsvp.RsvpId)
            {
                return BadRequest(ErrorMessageContracts.MismatchedId);
            }

            try
            {
                _context.Entry(rsvp).State = EntityState.Modified;
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
        public async Task<IActionResult> DeleteRsvp(int id)
        {
            var exists = await ExistsAsync(id);
            if (!exists)
            {
                return NotFound();
            }

            var rsvp = await _context.Rsvps.FindAsync(id);

            try
            {
                _context.Remove(rsvp);
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
            var item = await _context.Rsvps.FindAsync(id);

            return item != null;
        }
    }
}
