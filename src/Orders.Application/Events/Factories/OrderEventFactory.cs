using Orders.Application.DTOs;
using Orders.Application.Events.Orders;
using Orders.Core.Entities;

namespace Orders.Application.Events.Factories
{
    public static class OrderEventFactory
    {
        public static OrderCreatedProjectionEvent CreateOrderCreatedProjectionEvent(Order order)
        {
            var orderDto = new OrderDTO(order.Id, order.Code, order.CustomerId,
                                        order.TotalPrice, order.OrderItems, order.VoucherIsUsed, order.Discount,
                                        order.Address!, order.CreatedAt, order.OrderStatus, order.VoucherId);

            return new OrderCreatedProjectionEvent(orderDto);
        }
    }
}
