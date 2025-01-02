using System.Text.Json.Serialization;

namespace Orders.Application.DTOs
{
    public class OrderItemDTO
    {
        public OrderItemDTO(Guid id, Guid productId, string name, decimal price, string image, int quantity)
        {
            Id = id;
            ProductId = productId;
            Name = name;
            Price = price;
            Image = image;
            Quantity = quantity;
        }
        [JsonIgnore]
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public string Image { get; private set; }
        public int Quantity { get; private set; }
    }
}
