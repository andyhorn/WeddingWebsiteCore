using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingWebsiteCore.Models
{
    [Table("tiers")]
    public class Tier
    {
        public int TierId { get; set; }
        public string Name { get; set; }
        public ICollection<Family> Families { get; set; }
    }
}
