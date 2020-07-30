using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeddingWebsiteCore.Contracts;
using WeddingWebsiteCore.DataAccess;
using WeddingWebsiteCore.Models;

namespace WeddingWebsiteCore.Extensions
{
    public static class DatabaseExtensions
    {
        public static async Task Seed(this IHost host)
        {
            var email = GetDefaultEmail();
            var password = GetDefaultPassword();

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                return;

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var dbContext = services.GetRequiredService<WeddingContext>();

                var exists = await dbContext.Users
                    .FirstOrDefaultAsync(user => user.Email.Equals(email)) != null;

                if (exists)
                    return;

                var auth = services.GetRequiredService<Services.IAuthenticationService>();

                var user = new ApplicationUser
                {
                    FirstName = "DEFAULT",
                    LastName = "DEFAULT",
                    Email = email
                };

                auth.SetUserPassword(user, password);

                dbContext.Users.Add(user);
                dbContext.SaveChanges();
            }
        }

        private static string GetDefaultEmail()
        {
            return Environment.GetEnvironmentVariable(ApplicationConstants.DEFAULT_EMAIL);
        }

        private static string GetDefaultPassword()
        {
            return Environment.GetEnvironmentVariable(ApplicationConstants.DEFAULT_PASSWORD);
        }
    }
}
