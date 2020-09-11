using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingWebsiteCore.Models
{
    [Table("vendors")]
    public class Vendor
    {
        public int VendorId { get; set; }

        [Required, Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [DataType(DataType.PhoneNumber), Column("contactPhone")]
        public string ContactPhone { get; set; }

        [DataType(DataType.EmailAddress), Column("contactEmail")]
        public string ContactEmail { get; set; }

        [DataType(DataType.Url), Column("url")]
        public string Url { get; set; }

        // Address
        [ForeignKey(nameof(AddressId))]
        public Address Address { get; set; }

        [Column("FK_addressId")]
        public int? AddressId { get; set; }
    }
}
