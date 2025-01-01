using Orders.Application.Commands.Orders.Create;
using Orders.Application.DTOs;
using Orders.Core.Entities;
using Orders.Core.ValueObjects;

namespace Orders.Application.Mappers
{
    public static class OrderMapper
    {
        public static Order MapToEntity(this CreateOrderCommand command) =>
            new(command.CustomerId, command.TotalPrice, command.VoucherIsUsed, command.Discount);

        public static List<OrderItem> MapOrderItemToEntity(this List<OrderItemDTO> dto, Guid orderId) =>
            dto.Select(item => new OrderItem(orderId, item.ProductId, item.Name,
                       item.Quantity, item.Price, item.Image)).ToList();

        public static List<OrderItemDTO> MapOrderItemFromEntity(this List<OrderItem> dto) =>
            dto.Select(item => new OrderItemDTO(item.ProductId, item.ProductName,
                       item.UnitValue, item.ProductImage, item.Quantity)).ToList();

        public static Address MapToAddress(this AddressDTO dto) =>
            new(dto.Street, dto.Number, dto.AdditionalInfo, dto.Neighborhood, dto.ZipCode, dto.City, dto.State);

        public static Order MapToEntity(this OrderDTO command) =>
            new(command.Id, command.Code, command.CustomerId, command.TotalPrice,
                command.OrderItems, command.VoucherIsUsed, command.Discount, command.Address,
                command.CreatedAt, command.OrderStatus, command.VoucherId);
    }
}
