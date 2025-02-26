﻿using Orders.Application.Commands.Orders.Create;
using Orders.Application.DTOs;
using Orders.Core.Entities;
using Orders.Core.ValueObjects;

namespace Orders.Application.Mappers
{
    public static class OrderMapper
    {
        public static Order MapToEntity(this CreateOrderCommand command, Guid customerId) =>
            new(customerId, command.TotalPrice, command.VoucherIsUsed, command.Discount);

        public static List<OrderDTO> MapToEntity(this List<Order> orders) =>
                orders.Select(order => new OrderDTO(order.Id, order.Code, order.CustomerId,
                order.TotalPrice, order.OrderItems.MapOrderItemFromEntity(), order.VoucherIsUsed, order.Discount,
                order.Address!, order.CreatedAt, order.OrderStatus, order.VoucherId)).ToList();

        public static List<OrderItem> MapOrderItemToEntity(this List<OrderItemDTO> dto, Guid orderId) =>
            dto.Select(item => new OrderItem(orderId, item.ProductId, item.Name,
                       item.Quantity, item.Price, item.ImageUrl)).ToList();

        public static List<OrderItem> MapOrderItemEntityToProjection(this List<OrderItemDTO> dto, Guid orderId) =>
            dto.Select(item => new OrderItem(item.Id, orderId, item.ProductId, item.Name,
                   item.Quantity, item.Price, item.ImageUrl)).ToList();

        public static List<OrderItemDTO> MapOrderItemFromEntity(this List<OrderItem> dto) =>
            dto.Select(item => new OrderItemDTO(item.Id, item.ProductId, item.Name,
                       item.Price, item.ImageUrl, item.Quantity)).ToList();

        public static Address MapToAddress(this AddressDTO dto) =>
            new(dto.Street, dto.Number, dto.AdditionalInfo, dto.Neighborhood, dto.ZipCode, dto.City, dto.State);

        public static Order MapToEntity(this OrderDTO command) =>
            new(command.Id, command.Code, command.CustomerId, command.TotalPrice,
                command.OrderItems.MapOrderItemEntityToProjection(command.Id), command.VoucherIsUsed, command.Discount, command.Address,
                command.CreatedAt, command.OrderStatus, command.VoucherId);
    }
}
