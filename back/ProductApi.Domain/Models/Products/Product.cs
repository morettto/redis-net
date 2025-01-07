using System.Text.Json.Serialization;

namespace ProductApi.Domain.Models.Products
{
    public class Product
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
    }
}
