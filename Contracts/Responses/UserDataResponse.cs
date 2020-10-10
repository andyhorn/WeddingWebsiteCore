using WeddingWebsiteCore.Models;

namespace WeddingWebsiteCore.Contracts.Responses
{
    public class UserDataResponse
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public UserDataResponse(ApplicationUser user)
        {
            UserId = user.UserId;
            Email = user.Email;
            FirstName = user.FirstName;
            LastName = user.LastName;
        }
    }
}
