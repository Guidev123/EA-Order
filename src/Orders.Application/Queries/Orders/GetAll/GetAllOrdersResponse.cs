using Orders.Application.DTOs;
using Orders.Core.Enums;
using Orders.Core.ValueObjects;

namespace Orders.Application.Queries.Orders.GetAll
{
    public class GetAllOrdersResponse
    {
        public GetAllOrdersResponse(string code, bool voucherIsUsed, decimal discount,
                                    decimal totalPrice, DateTime createdAt, EOrderStatus orderStatus, 
                                    Address? address, List<OrderItemDTO> orderItems)
        {
            Code = code;
            VoucherIsUsed = voucherIsUsed;
            Discount = discount;
            TotalPrice = totalPrice;
            CreatedAt = createdAt;
            OrderStatus = orderStatus;
            Address = address;
            OrderItems = orderItems;
        }

        public string Code { get; private set; } = string.Empty;
        public bool VoucherIsUsed { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalPrice { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public EOrderStatus OrderStatus { get; private set; }
        public Address? Address { get; private set; }
        public List<OrderItemDTO> OrderItems { get; private set; } = [];
    }
}
