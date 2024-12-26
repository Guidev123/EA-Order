using Order.Core.DomainObjects;

namespace Order.Core.Entities
{
    public class OrderItem : Entity
    {
        public OrderItem(Guid orderId, Guid productId,
                         string productName, int quantity,
                         decimal price, string? productImage = null)
        {
            OrderId = orderId;
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            Price = price;
            ProductImage = productImage;
        }

        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public string? ProductImage { get; private set; }
    }
}
