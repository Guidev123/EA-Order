﻿using MediatR;
using Orders.Application.DTOs;
using Orders.Application.Responses;

namespace Orders.Application.Commands.Orders.Create
{
    public class CreateOrderCommand : IRequest<Response<CreateOrderResponse>>
    {
        public CreateOrderCommand(string customerId, decimal totalPrice,
                          List<OrderItemDTO> orderItems, string voucherCode,
                          bool voucherIsUsed, decimal discount, AddressDTO address)
        {
            CustomerId = customerId;
            TotalPrice = totalPrice;
            OrderItems = orderItems;
            VoucherCode = voucherCode;
            VoucherIsUsed = voucherIsUsed;
            Discount = discount;
            Address = address;
        }

        public string CustomerId { get; private set; }
        public decimal TotalPrice { get; private set; }
        public List<OrderItemDTO> OrderItems { get; private set; }
        public string VoucherCode { get; private set; }
        public bool VoucherIsUsed { get; private set; }
        public decimal Discount { get; private set; }
        public AddressDTO Address { get; private set; }

        public void SetCustomerId(string customerId) => CustomerId = customerId;
    }
}