using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingWebsiteCore.Models
{
    public class GuestWeddingRole
    {
        public int GuestWeddingRoleId { get; set; }
        [ForeignKey(nameof(GuestId))]
        public Guest Guest { get; set; }
        public int GuestId { get; set; }

        [ForeignKey(nameof(WeddingRoleId))]
        public WeddingRole WeddingRole { get; set; }
        public int WeddingRoleId { get; set; }
    }
}