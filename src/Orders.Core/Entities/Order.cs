using Orders.Core.DomainObjects;
using Orders.Core.Enums;
using Orders.Core.Events;
using Orders.Core.ValueObjects;

namespace Orders.Core.Entities
{
    public class Order : Entity, IAggregateRoot
    {
        public Order(Guid customerId, decimal totalPrice,
                     List<OrderItem> orderItems,
                     bool voucherIsUsed = false, decimal discount = 0)
        {
            Code = Guid.NewGuid().ToString()[..8];
            CustomerId = customerId;
            TotalPrice = totalPrice;
            OrderItems = orderItems;
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
        public List<OrderItem> OrderItems { get; private set; }

        public void ApplyVoucher(Voucher voucher)
        {
            VoucherIsUsed = true;
            Voucher = voucher;
            VoucherId = voucher.Id;
        }

        public void ApplyAddress(Address address) => Address = address;

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
    }
}
