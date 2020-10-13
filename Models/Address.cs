using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WeddingWebsiteCore.Models
{
    [Table("addresses")]
    public class Address
    {
        public int AddressId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Required, Column("streetNumber")]
        public string StreetNumber { get; set; }

        [Required, Column("streetName")]
        public string StreetName { get; set; }

        [Column("streetDetail")]
        public string StreetDetail { get; set; }

        [Required, Column("city")]
        public string City { get; set; }

        [Required, Column("state")]
        public string State { get; set; }

        [Required, Column("postalCode")]
        public string PostalCode { get; set; }

        [Required, Column("country")]
        public string Country { get; set; }

        [NotMapped]
        public string FullString
        {
            get
            {
                var builder = new StringBuilder();

                if (!string.IsNullOrWhiteSpace(StreetNumber))
                {
                    builder.Append(StreetNumber);
                }

                if (!string.IsNullOrWhiteSpace(StreetName))
                {
                    builder.Append(" ");
                    builder.Append(StreetName);
                }

                if (!string.IsNullOrWhiteSpace(StreetDetail))
                {
                    builder.Append(" ");
                    builder.Append(StreetDetail);
                }

                builder.Append(", ");

                if (!string.IsNullOrWhiteSpace(City))
                {
                    builder.Append(City);
                    builder.Append(", ");
                }

                if (!string.IsNullOrWhiteSpace(State))
                {
                    builder.Append(State);
                }

                if (!string.IsNullOrWhiteSpace(PostalCode))
                {
                    builder.Append(" ");
                    builder.Append(PostalCode);
                }

                return builder.ToString();
            }
        }

        public ICollection<Family> Families { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<Vendor> Vendors { get; set; }
    }
}
