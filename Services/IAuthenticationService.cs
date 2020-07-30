using System.IdentityModel.Tokens.Jwt;
using WeddingWebsiteCore.Models;

namespace WeddingWebsiteCore.Services
{
    public interface IAuthenticationService
    {
        string MakeToken(ApplicationUser user);
        bool AuthenticateToken(string token);
        JwtSecurityToken DecodeToken(string token);
        bool AuthenticatePassword(ApplicationUser user, string password);
        void SetUserPassword(ApplicationUser user, string password);
    }
}
