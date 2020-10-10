using System.ComponentModel.DataAnnotations;

namespace WeddingWebsiteCore.Contracts.Requests
{
    public class LoginRequest
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
