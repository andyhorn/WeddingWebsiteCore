using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingWebsiteCore.Models
{
    [Table("wedding_members")]
    public class WeddingMember : Guest
    {
        public ICollection<WeddingMemberRole> WeddingMemberRoles { get; set; }

        public WeddingMember()
        {
            IsWeddingMember = true;
        }
    }
}
