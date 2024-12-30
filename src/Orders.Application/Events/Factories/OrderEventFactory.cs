using Orders.Application.Events.OrderCreated;
using Orders.Core.Entities;

namespace Orders.Application.Events.Factories
{
    public static class OrderEventFactory
    {
        public static OrderCreatedEvent CreateOrderCreatedEvent(Order order) =>
             new(order.Id, order.Code, order.CustomerId, order.VoucherId,
                 order.VoucherIsUsed, order.Discount, order.TotalPrice, order.CreatedAt,
                 order.OrderStatus, order.Address,order.Voucher,order.OrderItems);
    }
}
