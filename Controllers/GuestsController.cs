using System;
using System.Collections.Generic;
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
    public class GuestsController : ControllerBase
    {
        private readonly WeddingContext _context;
        public GuestsController(WeddingContext context)
        {
            _context = context;
        }

        [HttpGet(RouteContracts.GetAll)]
        public async Task<IActionResult> GetAllGuests()
        {
            var guests = await _context.Guests
                .ToListAsync();

            return Ok(guests);
        }

        [HttpGet(RouteContracts.GetItem)]
        public async Task<IActionResult> GetGuest(int id)
        {
            var guest = await _context.Guests.FindAsync(id);

            if (guest == null)
            {
                return NotFound();
            }

            return Ok(guest);
        }

        [HttpPost(RouteContracts.PostItem)]
        public async Task<IActionResult> PostNewGuest([FromBody]Guest guest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            var existing = await _context.Guests.FindAsync(guest.GuestId);
            if (existing != null)
            {
                return BadRequest(ErrorMessageContracts.IdConflict);
            }

            try
            {
                await _context.AddAsync(guest);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction(nameof(PostNewGuest), guest, guest.GuestId);
        }

        [HttpPut(RouteContracts.PutItem)]
        public async Task<IActionResult> UpdateGuest(int id, [FromBody]Guest guest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            if (id != guest.GuestId)
            {
                return BadRequest(ErrorMessageContracts.MismatchedId);
            }

            var existing = await _context.Guests.FindAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            try
            {
                var updated = await UpdateGuest(guest);
                if (updated)
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete(RouteContracts.DeleteItem)]
        public async Task<IActionResult> DeleteGuest(int id)
        {
            var guest = await _context.Guests.FindAsync(id);

            if (guest == null)
            {
                return NotFound();
            }

            try
            {
                _context.Remove(guest);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        private async Task<bool> UpdateGuest(Guest guest)
        {
            try
            {
                var original = await _context.Guests.FindAsync(guest.GuestId);
                _context.Entry(original).State = EntityState.Detached;
                _context.Entry(guest).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }
}
