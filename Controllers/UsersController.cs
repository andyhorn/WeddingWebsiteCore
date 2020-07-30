using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet(RouteContracts.GetAll)]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _context.Users.ToListAsync();

            var response = new List<UserDataResponse>();

            users.ForEach(user => response.Add(new UserDataResponse(user)));

            return Ok(response);
        }

        [HttpGet(RouteContracts.GetSelf)]
        public async Task<IActionResult> GetUserFromToken([FromHeader(Name = "Authorization")]string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                return BadRequest(ErrorMessageContracts.MissingToken);
            }

            token = token.Split(" ").Last();

            var valid = _authenticationService.AuthenticateToken(token);
            if (!valid)
            {
                return BadRequest(ErrorMessageContracts.InvalidToken);
            }

            var decoded = _authenticationService.DecodeToken(token);

            var userIdClaim = decoded.Claims.FirstOrDefault(claim => claim.Type.Equals(Claims.UserId));
            var userId = userIdClaim.Value;

            if (string.IsNullOrWhiteSpace(userId))
            {
                return NotFound();
            }

            try
            {
                var user = await _context.Users.FindAsync(int.Parse(userId));
                var response = new UserDataResponse(user);
                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
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

                    var response = new LoginSuccessResponse(user, token);

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
