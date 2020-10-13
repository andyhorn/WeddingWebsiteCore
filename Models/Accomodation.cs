using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingWebsiteCore.Models
{
    [Table("accomodations")]
    public class Accomodation
    {
        [Key]
        public int AccomodationId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [ForeignKey(nameof(AddressId))]
        public Address Location { get; set; }
        public int AddressId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}