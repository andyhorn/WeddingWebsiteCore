using System.Collections;
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

        public string Name { get; set; }

        [Required]
        public string StreetNumber { get; set; }

        [Required]
        public string StreetName { get; set; }

        public string StreetDetail { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        public string Country { get; set; }

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
    }
}
