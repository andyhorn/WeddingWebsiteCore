using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingWebsiteCore.Contracts;
using WeddingWebsiteCore.Contracts.Requests;
using WeddingWebsiteCore.Contracts.Responses;
using WeddingWebsiteCore.DataAccess;
using WeddingWebsiteCore.Models;

namespace WeddingWebsiteCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private WeddingContext _context;
        private Services.IAuthenticationService _authenticationService;

        public UsersController(WeddingContext context, Services.IAuthenticationService authenticationService)
        {
            _context = context;
            _authenticationService = authenticationService;
        }

        [HttpPost(RouteContracts.Login)]
        public async Task<IActionResult> LoginUser([FromBody]LoginRequest loginRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values);
            }

            var exists = await EmailExistsAsync(loginRequest.Email);
            if (!exists)
            {
                return NotFound();
            }

            var user = await FindUserByEmailAsync(loginRequest.Email);

            try
            {
                var authenticated = _authenticationService.AuthenticatePassword(user, loginRequest.Password);
                if (authenticated)
                {
                    // return JWT
                    var token = _authenticationService.MakeToken(user);

                    var response = new LoginSuccessResponse
                    {
                        UserId = user.UserId,
                        Token = token,
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    };

                    return Ok(response);
                }
                else
                {
                    return Unauthorized();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        private async Task<ApplicationUser> FindUserByEmailAsync(string email)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(user => user.Email.Equals(email));
            return user;
        }

        private async Task<bool> EmailExistsAsync(string email)
        {
            var user = await FindUserByEmailAsync(email);
            return user != null;
        }
    }
}
