using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace WeddingWebsiteCore.Models
{
    [Table("families")]
    public class Family
    {
        public int FamilyId { get; set; }
        public string Name { get; set; }
        public ICollection<Guest> Members { get; set; }
        public int? HeadMemberId { get; set; }
        public int AdditionalGuests { get; set; }

        [ForeignKey(nameof(AddressId))]
        public Address Address { get; set; }

        public int? AddressId { get; set; }
        public Tier Tier { get; set; }
    }
}
