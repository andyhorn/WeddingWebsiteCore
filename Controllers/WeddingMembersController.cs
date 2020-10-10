using System;
using System.Collections.Generic;
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
    public class WeddingMembersController : ControllerBase
    {
        private WeddingContext _context;
        public WeddingMembersController(WeddingContext context)
        {
            _context = context;
        }

        [HttpGet(RouteContracts.GetAll)]
        public async Task<IActionResult> GetAllWeddingMembers()
        {
            var weddingMembers = await _context.WeddingMembers.ToListAsync();

            return Ok(weddingMembers);
        }

        [HttpGet(RouteContracts.GetItem)]
        public async Task<IActionResult> GetWeddingMember(int id)
        {
            var exists = await ExistsAsync(id);
            if (!exists)
            {
                return NotFound();
            }

            var weddingMember = await _context.WeddingMembers.FindAsync(id);

            return Ok(weddingMember);
        }

        [HttpPost(RouteContracts.PostItem)]
        public async Task<IActionResult> PostNewWeddingMember([FromBody]WeddingMember weddingMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            var exists = await ExistsAsync(weddingMember.GuestId);
            if (exists)
            {
                return BadRequest(ErrorMessageContracts.IdConflict);
            }

            try
            {
                await _context.AddAsync(weddingMember);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction(nameof(PostNewWeddingMember), weddingMember.GuestId);
        }

        [HttpPut(RouteContracts.PutItem)]
        public async Task<IActionResult> UpdateWeddingMember(int id, [FromBody]WeddingMember weddingMember)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            var exists = await ExistsAsync(id);
            if (!exists)
            {
                return NotFound();
            }

            if (id != weddingMember.GuestId)
            {
                return BadRequest(ErrorMessageContracts.MismatchedId);
            }

            try
            {
                _context.Entry(weddingMember).State = EntityState.Modified;
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
        public async Task<IActionResult> DeleteWeddingMember(int id)
        {
            var exists = await ExistsAsync(id);
            if (!exists)
            {
                return NotFound();
            }

            try
            {
                var weddingMember = await _context.WeddingMembers.FindAsync(id);
                _context.Remove(weddingMember);
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
            var weddingMember = await _context.WeddingMembers.FindAsync(id);

            return weddingMember != null;
        }
    }
}
