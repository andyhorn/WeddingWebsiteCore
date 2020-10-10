using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingWebsiteCore.Models
{
    [Table("images")]
    public class Image
    {
        public int ImageId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
