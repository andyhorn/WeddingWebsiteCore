using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingWebsiteCore.Models
{
    [Table("events")]
    public class Event
    {
        [Column("eventId"), Key]
        public int EventId { get; set; }

        [Required, Column("name")]
        public string Name { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Required, Column("startTime")]
        public DateTimeOffset StartTime { get; set; }

        [Column("endTime")]
        public DateTimeOffset? EndTime { get; set; }

        // Address
        [ForeignKey(nameof(AddressId))]
        public Address Address { get; set; }

        [Column("FK_addressId")]
        public int? AddressId { get; set; }
    }
}
