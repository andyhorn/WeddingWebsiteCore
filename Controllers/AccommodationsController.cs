using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingWebsiteCore.Contracts;
using WeddingWebsiteCore.DataAccess;
using WeddingWebsiteCore.Helpers;
using WeddingWebsiteCore.Models;

namespace WeddingWebsiteCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccommodationsController : ControllerBase
    {
        private readonly WeddingContext _context;

        public AccommodationsController(WeddingContext context)
        {
            _context = context;
        }

        [HttpGet(RouteContracts.GetAll)]
        public async Task<IActionResult> GetAllAccommodations()
        {
            var accommodations = await _context.Accommodations.ToListAsync();

            return Ok(accommodations);
        }

        [HttpGet(RouteContracts.GetItem)]
        public async Task<IActionResult> GetAccommodation(int id)
        {
            var accommodation = await _context.Accommodations.FindAsync(id);

            if (accommodation == null)
            {
                return NotFound();
            }

            return Ok(accommodation);
        }

        [HttpPost(RouteContracts.PostItem)]
        public async Task<IActionResult> PostNewAccommodation([FromBody] Accommodation accommodation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            var existing = await _context.Accommodations.FindAsync(accommodation.AccommodationId);
            if (existing != null)
            {
                return BadRequest(ErrorMessageContracts.IdConflict);
            }

            try
            {
                await _context.AddAsync(accommodation);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction(nameof(PostNewAccommodation), accommodation, accommodation.AccommodationId);
        }

        [HttpPut(RouteContracts.PutItem)]
        public async Task<IActionResult> UpdateAccommodation(int id, [FromBody] Accommodation accommodation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            if (id != accommodation.AccommodationId)
            {
                return BadRequest(ErrorMessageContracts.MismatchedId);
            }

            var existing = await _context.Accommodations.FindAsync(accommodation.AccommodationId);
            if (existing == null)
            {
                return NotFound();
            }

            try
            {
                AccommodationHelper.UpdateAccommodation(existing, accommodation);
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
        public async Task<IActionResult> DeleteAccommodation(int id)
        {
            var accommodation = await _context.Accommodations.FindAsync(id);

            if (accommodation == null)
            {
                return NotFound();
            }

            try
            {
                _context.Accommodations.Remove(accommodation);
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