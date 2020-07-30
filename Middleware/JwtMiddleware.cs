using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeddingWebsiteCore.Contracts;
using WeddingWebsiteCore.DataAccess;

namespace WeddingWebsiteCore.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        Services.IAuthenticationService _authenticationService;
        WeddingContext _context;
        public JwtMiddleware(RequestDelegate next, Services.IAuthenticationService authenticationService, WeddingContext context)
        {
            _next = next;
            _authenticationService = authenticationService;
            _context = context;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var token = httpContext.Request.Headers["Authorization"]
                .FirstOrDefault()?
                .Split(" ")
                .Last();

            if (token != null)
            {
                await AttachUserToHttpContext(httpContext, token);
            }

            await _next(httpContext);
        }

        private async Task AttachUserToHttpContext(HttpContext httpContext, string token)
        {
            var userId = _authenticationService.DecodeToken(token)
                .Claims
                .FirstOrDefault(claim => claim.Type.Equals(Claims.UserId));

            var user = await _context.Users.FindAsync(userId);

            httpContext.Items["User"] = user;
        }
    }
}
