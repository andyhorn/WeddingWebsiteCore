using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public async Task<IActionResult> PostNewGuest([FromBody] Guest guest)
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
                var uniqueInviteCode = await GetUniqueInviteCode();
                guest.InviteCode = uniqueInviteCode;

                await _context.AddAsync(guest);
                await _context.SaveChangesAsync();

                await SetAsHeadMemberIfAvailable(guest);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction(nameof(PostNewGuest), guest, guest.GuestId);
        }

        [HttpPut(RouteContracts.PutItem)]
        public async Task<IActionResult> UpdateGuest(int id, [FromBody] Guest guest)
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
                    await SetAsHeadMemberIfAvailable(guest);
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
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private async Task SetAsHeadMemberIfAvailable(Guest guest)
        {
            if (!guest.FamilyId.HasValue)
                return;

            var family = await _context.Families
                .Include(family => family.Members)
                .FirstOrDefaultAsync(family => family.FamilyId.Equals(guest.FamilyId));

            if (family == null || family.HeadMemberId.HasValue)
                return;

            family.HeadMemberId = guest.GuestId;

            await _context.SaveChangesAsync();
        }

        private async Task<string> GetUniqueInviteCode()
        {
            string code = string.Empty;

            while (string.IsNullOrWhiteSpace(code))
            {
                var newCode = GetInviteCode();

                var inUse = await _context.Guests
                    .FirstOrDefaultAsync(guest => guest.InviteCode.Equals(newCode));

                if (inUse == null)
                {
                    code = newCode;
                }
            }

            return code;
        }

        private string GetInviteCode()
        {
            const int length = 6;
            const int minimum = 65;
            const int maximum = 90;

            var builder = new StringBuilder();
            var random = new Random();

            for (var i = 0; i < length; i++)
            {
                var val = random.Next(minimum, maximum);
                builder.Append((char)val);
            }

            return builder.ToString();
        }
    }
}
