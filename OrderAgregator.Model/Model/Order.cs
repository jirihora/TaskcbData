﻿using System.Text.Json.Serialization;

namespace OrderAggregator.Model.Model
{
    public class Order
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonPropertyName("productId")]
        public string ProductId { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}
