using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingWebsiteCore.Models
{
    [Table("categories")]
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey(nameof(ParentId))]
        public Category Parent { get; set; }
        public int ParentId { get; set; }
    }
}