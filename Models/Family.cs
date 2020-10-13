using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingWebsiteCore.Models
{
    [Table("families")]
    public class Family
    {
        public int FamilyId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("FK_headMemberId")]
        public int? HeadMemberId { get; set; }

        [Column("additionalGuests")]
        public int AdditionalGuests { get; set; }

        // Address
        [ForeignKey(nameof(AddressId))]
        public Address Address { get; set; }

        [Column("FK_addressId")]
        public int? AddressId { get; set; }

        // Tier
        [ForeignKey(nameof(TierId))]
        public Tier Tier { get; set; }

        [Column("FK_tierId")]
        public int? TierId { get; set; }

        // Members
        public ICollection<Guest> Members { get; set; }
    }
}
