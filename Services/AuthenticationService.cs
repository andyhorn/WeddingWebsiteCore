﻿using JWT.Algorithms;
using JWT.Builder;
using JWT.Exceptions;
using System;
using System.Text;
using WeddingWebsiteCore.Contracts;
using WeddingWebsiteCore.Models;

namespace WeddingWebsiteCore.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public bool AuthenticatePassword(ApplicationUser user, string password)
        {
            var authenticated = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            return authenticated;
        }

        public string MakeToken(ApplicationUser user)
        {
            var secret = GetApplicationSecret();

            var token = new JwtBuilder()
                .WithAlgorithm(new HMACSHA256Algorithm())
                .WithSecret(secret)
                .AddClaim(Claims.Expiration, DateTimeOffset.UtcNow.AddMonths(1).ToUnixTimeSeconds())
                .AddClaim(Claims.UserId, user.UserId)
                .Encode();

            return token;
        }

        public bool AuthenticateToken(string token)
        {
            var secret = GetApplicationSecret();

            try
            {
                var json = new JwtBuilder()
                    .WithAlgorithm(new HMACSHA256Algorithm())
                    .WithSecret(secret)
                    .MustVerifySignature()
                    .Decode(token);
            }
            catch (TokenExpiredException)
            {
                return false;
            }
            catch (SignatureVerificationException)
            {
                return false;
            }

            return true;
        }

        public void SetUserPassword(ApplicationUser user, string password)
        {
            var hash = BCrypt.Net.BCrypt.HashPassword(password);
            user.PasswordHash = hash;
        }

        private string GetApplicationSecret()
        {
            var secret = System.Environment.GetEnvironmentVariable(ApplicationConstants.TOKEN_KEY);
            
            return secret;
        }
    }
}
