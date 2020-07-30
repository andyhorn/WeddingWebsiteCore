namespace WeddingWebsiteCore.Contracts.Responses
{
    public class LoginSuccessResponse
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
