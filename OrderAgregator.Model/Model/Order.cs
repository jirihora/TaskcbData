using System.Text.Json.Serialization;

namespace OrderAgregator.Model.Model
{
    public class Order
    {
        [JsonPropertyName("productId")]
        public string ProductId { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}
