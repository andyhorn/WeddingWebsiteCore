using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingWebsiteCore.Models
{
    [Table("vendors")]
    public class Vendor
    {
        public int VendorId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string ContactPhone { get; set; }

        [DataType(DataType.EmailAddress)]
        public string ContactEmail { get; set; }

        [DataType(DataType.Url)]
        public string Url { get; set; }

        public int? AddressId { get; set; }
    }
}
