using Orders.Application.Mappers;
using Orders.Application.Queries.Orders.GetAll;
using Orders.Core.Entities;

namespace Orders.Application.Queries.Factories
{
    public static class OrderQueryFactory
    {
        public static List<GetAllOrdersResponse> CreateGetAllOrdersQuery(List<Order> orders) =>
            orders.Select(order =>
            new GetAllOrdersResponse(order.Code, order.VoucherIsUsed,
            order.Discount, order.TotalPrice,
            order.CreatedAt, order.OrderStatus,
            order.Address, order.OrderItems.MapOrderItemFromEntity())).ToList();
    }
}
