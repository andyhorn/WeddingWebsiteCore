using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingWebsiteCore.Models
{
    [Table("guests")]
    public class Guest
    {
        [Key]
        public int GuestId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public bool IsWeddingMember { get; set; } = false;
        public bool IsChild { get; set; } = false;
        public int? ParentId { get; set; }

        // Family
        [ForeignKey(nameof(FamilyId))]
        public Family Family { get; set; }
        public int? FamilyId { get; set; }

        public ICollection<Rsvp> RSVPs { get; set; }
    }
}
