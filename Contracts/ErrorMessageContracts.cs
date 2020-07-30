namespace WeddingWebsiteCore.Contracts
{
    public static class ErrorMessageContracts
    {
        public static string IdConflict = "An item with that ID already exists.";
        public const string MismatchedId = "The ID in the URL does not match the ID of the item.";
        public const string MissingToken = "Security token is required.";
        public const string InvalidToken = "The provided security token is invalid.";
    }
}
