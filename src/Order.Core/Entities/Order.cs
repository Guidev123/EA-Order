using Order.Core.DomainObjects;
using Order.Core.Enums;
using Order.Core.ValueObjects;

namespace Order.Core.Entities
{
    public class Order : Entity, IAggregateRoot
    {
        public Order(string customerId, decimal totalPrice,
                     List<OrderItem> orderItems, decimal discount = 0)
        {
            Code = Guid.NewGuid().ToString()[..8];
            CustomerId = customerId;
            TotalPrice = totalPrice;
            _items = orderItems;
            Discount = discount;
            CreatedAt = DateTime.Now;
            OrderStatus = EOrderStatus.WaitingPyment;
        }

        public string Code { get; private set; } = string.Empty;
        public string CustomerId { get; private set; } = string.Empty;
        public decimal Discount { get; private set; }
        public decimal TotalPrice { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public EOrderStatus OrderStatus { get; private set; }
        public Address? Address { get; private set; }
        private readonly List<OrderItem> _items = [];
        public IReadOnlyCollection<OrderItem> OrderItems => _items;
        public void ApplyAddress(Address address) => Address = address;
        public void AuthorizeOrder() => OrderStatus = EOrderStatus.Authorized;
        public void CancelOrder() => OrderStatus = EOrderStatus.Canceled;
        public void PayOrder() => OrderStatus = EOrderStatus.Paid;
        public void DeliveryOrder() => OrderStatus = EOrderStatus.Delivered;
    }
}
