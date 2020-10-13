using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingWebsiteCore.Models
{
    [Table("wedding_roles")]
    public class WeddingRole
    {
        public int WeddingRoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<WeddingMemberRole> WeddingMemberRoles { get; set; }
    }
}
