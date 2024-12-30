using Orders.Core.Entities;
using Orders.Core.Enums;
using Orders.Core.ValueObjects;

namespace Orders.Core.Events
{
    public class OrderCreatedProjectionEvent : IDomainEvent
    {
        public OrderCreatedProjectionEvent(Guid id, string code, Guid customerId,
                                Guid? voucherId, bool voucherIsUsed, decimal discount,
                                decimal totalPrice, DateTime createdAt, EOrderStatus orderStatus,
                                Address? address, Voucher? voucher, List<OrderItem> orderItems)
        {
            Id = id;
            Code = code;
            CustomerId = customerId;
            VoucherId = voucherId;
            VoucherIsUsed = voucherIsUsed;
            Discount = discount;
            TotalPrice = totalPrice;
            CreatedAt = createdAt;
            OrderStatus = orderStatus;
            Address = address;
            Voucher = voucher;
            OrderItems = orderItems;
        }

        public Guid Id { get; }
        public string Code { get; }
        public Guid CustomerId { get; }
        public Guid? VoucherId { get; }
        public bool VoucherIsUsed { get; }
        public decimal Discount { get; }
        public decimal TotalPrice { get; }
        public DateTime CreatedAt { get; }
        public EOrderStatus OrderStatus { get; }
        public Address? Address { get; }
        public Voucher? Voucher { get; }
        public List<OrderItem> OrderItems { get; }

        public Guid EventId => Guid.NewGuid();
        public DateTime OccurredAt => DateTime.Now;
    }
}
