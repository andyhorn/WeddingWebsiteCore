using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingWebsiteCore.Models
{
    [Table("rsvps")]
    public class Rsvp
    {
        public int RsvpId { get; set; }
        public bool HasResponded { get; set; } = false;
        public bool IsAttending { get; set; } = false;

        [ForeignKey(nameof(GuestId))]
        public Guest Guest { get; set; }
        public int GuestId { get; set; }

        [ForeignKey(nameof(EventId))]
        public Event Event { get; set; }
        public int EventId { get; set; }
    }
}
