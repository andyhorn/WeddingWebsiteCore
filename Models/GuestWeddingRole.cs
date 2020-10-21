using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingWebsiteCore.Models
{
    public class GuestWeddingRole
    {
        [ForeignKey(nameof(GuestId))]
        public Guest Guest { get; set; }
        public int GuestId { get; set; }

        [ForeignKey(nameof(WeddingRoleId))]
        public WeddingRole WeddingRole { get; set; }
        public int WeddingRoleId { get; set; }
    }
}