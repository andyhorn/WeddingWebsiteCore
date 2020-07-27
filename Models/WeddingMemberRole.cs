using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingWebsiteCore.Models
{
    [Table("wedding_member_roles")]
    public class WeddingMemberRole
    {
        public int WeddingMemberRoleId { get; set; }

        [ForeignKey(nameof(WeddingMemberId))]
        public WeddingMember WeddingMember { get; set; }
        public int WeddingMemberId { get; set; }

        [ForeignKey(nameof(WeddingRoleId))]
        public WeddingRole WeddingRole { get; set; }
        public int WeddingRoleId { get; set; }
    }
}
