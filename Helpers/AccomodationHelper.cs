using WeddingWebsiteCore.Models;

namespace WeddingWebsiteCore.Helpers
{
    public class AccomodationHelper
    {
        public static void UpdateAccomodation(Accomodation existing, Accomodation update)
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