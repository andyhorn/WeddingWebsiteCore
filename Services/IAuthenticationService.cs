using WeddingWebsiteCore.Models;

namespace WeddingWebsiteCore.Services
{
    public interface IAuthenticationService
    {
        string MakeToken(ApplicationUser user);
        bool AuthenticateToken(string token);
        bool AuthenticatePassword(ApplicationUser user, string password);
        void SetUserPassword(ApplicationUser user, string password);
    }
}
