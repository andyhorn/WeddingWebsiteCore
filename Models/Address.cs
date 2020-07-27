using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingWebsiteCore.Models
{
    [Table("addresses")]
    public class Address
    {
        public int AddressId { get; set; }
        public string Name { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string StreetDetail { get; set; }

        [Column("State", TypeName = "char(2)")]
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public ICollection<Family> Families { get; set; }
        public ICollection<Vendor> Vendors { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
