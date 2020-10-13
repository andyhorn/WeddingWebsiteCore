using WeddingWebsiteCore.Models;

namespace WeddingWebsiteCore.Helpers
{
    public class AddressHelper
    {
        public static void UpdateAddress(Address existing, Address update)
        {
            if (existing == null || update == null)
            {
                return;
            }

            existing.City = update.City;
            existing.Country = update.Country;
            existing.Name = update.Name;
            existing.PostalCode = update.PostalCode;
            existing.State = update.State;
            existing.StreetDetail = update.StreetDetail;
            existing.StreetName = update.StreetName;
            existing.StreetNumber = update.StreetNumber;
        }
    }
}