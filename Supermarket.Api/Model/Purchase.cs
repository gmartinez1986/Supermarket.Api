using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Supermarket.Api.Model
{
    public class Purchase
    {
        [JsonIgnore]
        public int Id { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public int IdProduct { get; set; }
        [Required]
        public string Total { get; set; }
    }
}
