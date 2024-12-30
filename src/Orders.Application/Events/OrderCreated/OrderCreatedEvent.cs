using MediatR;
using Orders.Core.Entities;
using Orders.Core.Enums;
using Orders.Core.Events;
using Orders.Core.ValueObjects;

namespace Orders.Application.Events.OrderCreated
{
    public class OrderCreatedEvent : Event, INotification
    {
        public OrderCreatedEvent(Guid id, string code, Guid customerId,
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

        public Guid Id { get; private set; }
        public string Code { get; private set; } = string.Empty;
        public Guid CustomerId { get; private set; }
        public Guid? VoucherId { get; private set; }
        public bool VoucherIsUsed { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalPrice { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public EOrderStatus OrderStatus { get; private set; }
        public Address? Address { get; private set; }
        public Voucher? Voucher { get; private set; }
        public List<OrderItem> OrderItems { get; private set; }
    }
}
