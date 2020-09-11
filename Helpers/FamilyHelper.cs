using WeddingWebsiteCore.Models;

namespace WeddingWebsiteCore.Helpers
{
    public class FamilyHelper
    {
        public static void UpdateFamily(Family original, Family update)
        {
            if (original == null || update == null)
            {
                return;
            }

            original.AdditionalGuests = update.AdditionalGuests;
            original.HeadMemberId = update.HeadMemberId;
            original.Name = update.Name;
            original.AddressId = update.AddressId;
        }
    }
}
