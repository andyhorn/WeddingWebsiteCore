﻿using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using WeddingWebsiteCore.Contracts;
using WeddingWebsiteCore.DataAccess;
using WeddingWebsiteCore.Helpers;
using WeddingWebsiteCore.Models;

namespace WeddingWebsiteCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly WeddingContext _context;
        public ImagesController(WeddingContext context)
        {
            _context = context;
        }

        [HttpGet(RouteContracts.GetAll)]
        public async Task<IActionResult> GetAllImages()
        {
            var images = await _context.Images.ToListAsync();

            return Ok(images);
        }

        [HttpGet(RouteContracts.GetItem)]
        public async Task<IActionResult> GetImage(int id)
        {
            var image = await _context.Images.FindAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            return Ok(image);
        }

        [HttpPost(RouteContracts.PostItem)]
        public async Task<IActionResult> PostNewImage()
        {
            var imageFile = Request.Form.Files[0];
            
            var image = ImageFactory.FromFile(imageFile);

            try
            {
                await _context.AddAsync(image);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction(nameof(PostNewImage), image.ImageId);
        }

        [HttpPut(RouteContracts.PutItem)]
        public async Task<IActionResult> UpdateImage(int id, [FromBody]Image image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            if (id != image.ImageId)
            {
                return BadRequest(ErrorMessageContracts.MismatchedId);
            }

            var existingImage = await _context.Images.FindAsync(id);
            if (existingImage == null)
            {
                return NotFound();
            }

            try
            {
                _context.Entry(image).State = EntityState.Modified;
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
        public async Task<IActionResult> DeleteImage(int id)
        {
            var image = await _context.Images.FindAsync(id);

            if (image == null)
            {
                return NotFound();
            }

            try
            {
                _context.Remove(image);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }
    }
}
