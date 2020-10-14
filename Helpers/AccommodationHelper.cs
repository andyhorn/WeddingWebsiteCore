using WeddingWebsiteCore.Models;

namespace WeddingWebsiteCore.Helpers
{
    public class AccommodationHelper
    {
        public static void UpdateAccommodation(Accommodation existing, Accommodation update)
        {
            if (existing == null || update == null)
            {
                return;
            }

            existing.AddressId = update.AddressId;
            existing.CategoryId = update.CategoryId;
            existing.Description = update.Description;
            existing.Name = update.Name;
        }
    }
}