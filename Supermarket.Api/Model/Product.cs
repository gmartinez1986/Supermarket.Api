using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace Supermarket.Api.Model
{
    public class Product
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public DateTime DateOfExpirity { get; set; }
        [Required]
        public string Price { get; set; }
    }
}
