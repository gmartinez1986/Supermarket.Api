using System.Xml.Linq;

namespace Supermarket.Api.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public DateTime DateOfExpirity { get; set; }
        public decimal Price { get; set; }
    }
}
