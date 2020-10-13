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
    public class VendorsController : ControllerBase
    {
        private WeddingContext _context;
        public VendorsController(WeddingContext context)
        {
            _context = context;
        }

        [HttpGet(RouteContracts.GetAll)]
        public async Task<IActionResult> GetAllVendors()
        {
            var vendors = await _context.Vendors.ToListAsync();

            return Ok(vendors);
        }

        [HttpGet(RouteContracts.GetItem)]
        public async Task<IActionResult> GetVendor(int id)
        {
            var exists = await ExistsAsync(id);
            if (!exists)
            {
                return NotFound();
            }

            var vendor = await _context.Vendors.FindAsync(id);

            return Ok(vendor);
        }

        [HttpPost(RouteContracts.PostItem)]
        public async Task<IActionResult> PostNewVendor([FromBody]Vendor vendor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            var exists = await ExistsAsync(vendor.VendorId);
            if (exists)
            {
                return BadRequest(ErrorMessageContracts.IdConflict);
            }

            try
            {
                await _context.AddAsync(vendor);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction(nameof(PostNewVendor), vendor.VendorId);
        }

        [HttpPut(RouteContracts.PutItem)]
        public async Task<IActionResult> UpdateVendor(int id, [FromBody]Vendor vendor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            var exists = await ExistsAsync(vendor.VendorId);
            if (!exists)
            {
                return NotFound();
            }

            if (id != vendor.VendorId)
            {
                return BadRequest(ErrorMessageContracts.MismatchedId);
            }

            try
            {
                _context.Entry(vendor).State = EntityState.Modified;
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
        public async Task<IActionResult> DeleteVendor(int id)
        {
            var exists = await ExistsAsync(id);
            if (!exists)
            {
                return NotFound();
            }

            try
            {
                var vendor = await _context.Vendors.FindAsync(id);
                _context.Remove(vendor);
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
            try
            {
                var vendor = await _context.Rsvps.FindAsync(id);
                return vendor != null;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
