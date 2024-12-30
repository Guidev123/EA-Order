using Orders.Core.Entities;
using Orders.Core.Events;

namespace Orders.Application.Events.Factories
{
    public static class OrderEventFactory
    {
        public static OrderCreatedProjectionEvent CreateOrderCreatedEvent(Order order) =>
             new(order.Id, order.Code, order.CustomerId, order.VoucherId,
                 order.VoucherIsUsed, order.Discount, order.TotalPrice, order.CreatedAt,
                 order.OrderStatus, order.Address,order.Voucher,order.OrderItems);
    }
}
