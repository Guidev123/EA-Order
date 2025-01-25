using Orders.Core.DomainObjects;

namespace Orders.Core.Entities
{
    public class OrderItem : Entity
    {
        public OrderItem(Guid orderId, Guid productId,
                         string name, int quantity,
                         decimal price, string productImage)
        {
            OrderId = orderId;
            ProductId = productId;
            Name = name;
            Quantity = quantity;
            Price = price;
            ImageUrl = productImage;
        }

        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public string ImageUrl { get; private set; }
        internal decimal GetPrice() => Quantity * Price;

        // CTOR mapping
        public OrderItem(Guid id, Guid orderId, Guid productId,
                 string productName, int quantity,
                 decimal unitValue, string productImage)
        {
            Id = id;
            OrderId = orderId;
            ProductId = productId;
            Name = productName;
            Quantity = quantity;
            Price = unitValue;
            ImageUrl = productImage;
        }
    }
}
