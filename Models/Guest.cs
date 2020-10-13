using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

        // Child-Parent Relationships
        public bool IsChild { get; set; } = false;
        public bool IsUnderTen { get; set; }

        [ForeignKey(nameof(ParentId)), NotMapped, JsonIgnore]
        public Guest Parent { get; set; }
        public int? ParentId { get; set; }

        [NotMapped, JsonIgnore]
        public ICollection<Guest> Children { get; set; }

        // Family
        [ForeignKey(nameof(FamilyId)), NotMapped, JsonIgnore]
        public Family Family { get; set; }
        public int? FamilyId { get; set; }

        // RSVPs
        [JsonIgnore]
        public ICollection<Rsvp> RSVPs { get; set; }
        public string InviteCode { get; set; }
    }
}
