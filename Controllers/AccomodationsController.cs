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
    public class AccomodationsController : ControllerBase
    {
        private readonly WeddingContext _context;

        public AccomodationsController(WeddingContext context)
        {
            _context = context;
        }

        [HttpGet(RouteContracts.GetAll)]
        public async Task<IActionResult> GetAllAccomodations()
        {
            var accomodations = await _context.Accomodations.ToListAsync();

            return Ok(accomodations);
        }

        [HttpGet(RouteContracts.GetItem)]
        public async Task<IActionResult> GetAccomodation(int id)
        {
            var accomodation = await _context.Accomodations.FindAsync(id);

            if (accomodation == null)
            {
                return NotFound();
            }

            return Ok(accomodation);
        }

        [HttpPost(RouteContracts.PostItem)]
        public async Task<IActionResult> PostNewAccomodation([FromBody] Accomodation accomodation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            var existing = await _context.Accomodations.FindAsync(accomodation.AccomodationId);
            if (existing != null)
            {
                return BadRequest(ErrorMessageContracts.IdConflict);
            }

            try
            {
                await _context.AddAsync(accomodation);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction(nameof(PostNewAccomodation), accomodation, accomodation.AccomodationId);
        }

        [HttpPut(RouteContracts.PutItem)]
        public async Task<IActionResult> UpdateAccomodation(int id, [FromBody] Accomodation accomodation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            if (id != accomodation.AccomodationId)
            {
                return BadRequest(ErrorMessageContracts.MismatchedId);
            }

            var existing = await _context.Accomodations.FindAsync(accomodation.AccomodationId);
            if (existing == null)
            {
                return NotFound();
            }

            try
            {
                AccomodationHelper.UpdateAccomodation(existing, accomodation);
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
        public async Task<IActionResult> DeleteAccomodation(int id)
        {
            var accomodation = await _context.Accomodations.FindAsync(id);

            if (accomodation == null)
            {
                return NotFound();
            }

            try
            {
                _context.Accomodations.Remove(accomodation);
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