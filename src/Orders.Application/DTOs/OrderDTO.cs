using Orders.Core.Entities;
using Orders.Core.Enums;
using Orders.Core.ValueObjects;

namespace Orders.Application.DTOs
{
    public record OrderDTO(Guid Id, string Code, Guid CustomerId,
                           decimal TotalPrice, List<OrderItem> OrderItems, bool VoucherIsUsed,
                           decimal? Discount, Address Address,
                           DateTime CreatedAt, EOrderStatus OrderStatus, Guid VoucherId);
}
