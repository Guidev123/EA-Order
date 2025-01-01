using Orders.Core.DomainObjects;

namespace Orders.Core.Entities
{
    public class OrderItem : Entity
    {
        public OrderItem(Guid orderId, Guid productId,
                         string productName, int quantity,
                         decimal unitValue, string productImage)
        {
            OrderId = orderId;
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitValue = unitValue;
            ProductImage = productImage;
        }

        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitValue { get; private set; }
        public string ProductImage { get; private set; }
        internal decimal GetPrice() => Quantity * UnitValue;

        // CTOR mapping
        public OrderItem(Guid id, Guid orderId, Guid productId,
                 string productName, int quantity,
                 decimal unitValue, string productImage)
        {
            Id = id;
            OrderId = orderId;
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitValue = unitValue;
            ProductImage = productImage;
        }
    }
}
