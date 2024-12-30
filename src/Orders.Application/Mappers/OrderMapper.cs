using Orders.Application.Commands.Orders.Create;
using Orders.Application.DTOs;
using Orders.Core.Entities;
using Orders.Core.ValueObjects;

namespace Orders.Application.Mappers
{
    public static class OrderMapper
    {
        public static Order MapToEntity(this CreateOrderCommand command) =>
            new(command.CustomerId, command.TotalPrice, command.OrderItems.MapOrderItemToEntity(), command.VoucherIsUsed, command.Discount);
        private static List<OrderItem> MapOrderItemToEntity(this List<OrderItemDTO> dto) =>
            dto.Select(item => new OrderItem(item.OrderId, item.ProductId, item.Name, item.Quantity, item.Price, item.Image)).ToList();
        public static Address MapToAddress(this AddressDTO dto) =>
            new(dto.Street, dto.Number, dto.AdditionalInfo, dto.Neighborhood, dto.ZipCode, dto.City, dto.State);
    }
}
