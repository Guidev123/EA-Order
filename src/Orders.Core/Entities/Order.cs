using Orders.Core.DomainObjects;
using Orders.Core.Enums;
using Orders.Core.Events;
using Orders.Core.ValueObjects;

namespace Orders.Core.Entities
{
    public class Order : Entity, IAggregateRoot
    {
        public Order() { }
        public Order(Guid customerId, decimal totalPrice,
                     bool voucherIsUsed = false, decimal discount = 0)
        {
            Code = Guid.NewGuid().ToString()[..8];
            CustomerId = customerId;
            TotalPrice = totalPrice;
            Discount = discount;
            VoucherIsUsed = voucherIsUsed;
            CreatedAt = DateTime.Now;
            OrderStatus = EOrderStatus.WaitingPyment;
            AddEvent(new OrderCreatedEvent(CustomerId));
        }

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
        public List<OrderItem> OrderItems { get; private set; } = [];

        public void ApplyVoucher(Voucher voucher)
        {
            VoucherIsUsed = true;
            Voucher = voucher;
            VoucherId = voucher.Id;
        }

        public void AddItems(List<OrderItem> orderItems) =>
            OrderItems.AddRange(orderItems);

        public void ApplyAddress(Address address)
        {
            AddEvent(new ReceivedAddressEvent(address));
            Address = address;
        }
        public void CalculateOrderPrice()
        {
            TotalPrice = OrderItems.Sum(p => p.GetPrice());
            CalculateOrderPriceDiscount();
        }

        public void CalculateOrderPriceDiscount()
        {
            if (!VoucherIsUsed) return;

            decimal discount = 0;
            var value = TotalPrice;

            if (Voucher!.DiscountType == EDiscountType.Percentual)
            {
                if (Voucher.Percentual.HasValue)
                {
                    discount = (value * Voucher.Percentual.Value) / 100;
                    value -= discount;
                }
            }
            else
            {
                if (Voucher.Percentual.HasValue)
                {
                    discount = Voucher.DiscountValue!.Value;
                    value -= discount;
                }
            }

            TotalPrice = value < 0 ? 0 : value;
            Discount = discount;
        }

        public void AuthorizeOrder() => OrderStatus = EOrderStatus.Authorized;
        public void CancelOrder() => OrderStatus = EOrderStatus.Canceled;
        public void PayOrder() => OrderStatus = EOrderStatus.Paid;
        public void DeliveryOrder() => OrderStatus = EOrderStatus.Delivered;

        // CTOR mapping
        public Order(Guid id, string code, Guid customerId,
                     decimal totalPrice, List<OrderItem> orderItems, bool voucherIsUsed,
                     decimal? discount, Address address,
                     DateTime createdAt, EOrderStatus orderStatus, Guid? voucherId)
        {
            Id = id;
            Code = code;
            CustomerId = customerId;
            TotalPrice = totalPrice;
            OrderItems = orderItems;
            VoucherIsUsed = voucherIsUsed;
            Discount = discount ?? 0;
            Address = address;
            CreatedAt = createdAt;
            OrderStatus = orderStatus;
            VoucherId = voucherId;
        }
    }
}
