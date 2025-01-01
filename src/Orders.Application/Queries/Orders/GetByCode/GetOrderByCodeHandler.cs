using MediatR;
using Orders.Application.Mappers;
using Orders.Application.Responses;
using Orders.Core.Repositories;

namespace Orders.Application.Queries.Orders.GetByCode
{
    public sealed class GetOrderByCodeHandler(IUnitOfWork unitOfWork)
                      : IRequestHandler<GetOrderByCodeQuery, Response<GetOrderByCodeResponse>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<Response<GetOrderByCodeResponse>> Handle(GetOrderByCodeQuery request, CancellationToken cancellationToken)
        {
            var order = await _unitOfWork.Orders.GetByCodeAsync(request.Code);
            if(order is null)
                return new(null, 404, "Order not found");

            var orderItems = order.OrderItems.MapOrderItemFromEntity();

            var result = new GetOrderByCodeResponse(order.Code, order.VoucherIsUsed, order.Discount,
                                                    order.TotalPrice, order.CreatedAt, order.OrderStatus,
                                                    order.Address, orderItems);

            return new(result, 200);
        }
    }
}
