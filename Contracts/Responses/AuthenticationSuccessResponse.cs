using WeddingWebsiteCore.Models;

namespace WeddingWebsiteCore.Contracts.Responses
{
    public class AuthenticationSuccessResponse
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public AuthenticationSuccessResponse(ApplicationUser user, string token)
        {
            UserId = user.UserId;
            Token = token;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }
    }
}
