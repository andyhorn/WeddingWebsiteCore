using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingWebsiteCore.Models
{
    [Table("registry_icons")]
    public class RegistryIcon
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }

        [Required]
        public byte[] Data { get; set; }
    }
}