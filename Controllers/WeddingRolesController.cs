﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingWebsiteCore.DataAccess;
using WeddingWebsiteCore.Models;

namespace WeddingWebsiteCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeddingRolesController : ControllerBase
    {
        private WeddingContext _context { get; set; }
        public WeddingRolesController(WeddingContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _context.WeddingRoles.ToListAsync();

            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRole(int id)
        {
            var role = await _context.FindAsync<WeddingRole>(id);

            if (role == null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        [HttpPost]
        public async Task<IActionResult> PostNewRole([FromBody]WeddingRole role)
        {
            if (role.WeddingRoleId != 0)
            {
                return BadRequest("Cannot create an existing object");
            }

            try
            {
                await _context.AddAsync(role);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction("PostNewRole", role, role.WeddingRoleId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody]WeddingRole role)
        {
            var item = await _context.FindAsync<WeddingRole>(role.WeddingRoleId);
            if (item == null)
            {
                return NotFound();
            }

            try
            {
                _context.Entry(role).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _context.FindAsync<WeddingRole>(id);

            if (role == null)
            {
                return NotFound();
            }

            try
            {
                _context.Remove(role);
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
