using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingWebsiteCore.Models
{
    [Table("registries")]
    public class Registry
    {
        public int RegistryId { get; set; }
        public string Name { get; set; }

        [DataType(DataType.Url)]
        public string Url { get; set; }
    }
}
