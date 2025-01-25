using MediatR;
using Orders.Application.Queries.Factories;
using Orders.Application.Responses;
using Orders.Application.Responses.Messages;
using Orders.Core.Repositories;

namespace Orders.Application.Queries.Orders.GetAll
{
    public sealed class GetAllOrdersHandler(IUnitOfWork unitOfWork)
                      : IRequestHandler<GetAllOrdersQuery, PagedResponse<List<GetAllOrdersResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        public async Task<PagedResponse<List<GetAllOrdersResponse>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _unitOfWork.Orders.GetAllAsync(request.PageNumber, request.PageSize, request.CustomerId);
            if (orders is null)
                return new(null, 404, ResponseMessages.NO_ORDERS_FOUND.GetDescription());

            var orderResponses = OrderQueryFactory.CreateGetAllOrdersQuery(orders);

            return new(orderResponses.Count, orderResponses, request.PageNumber, request.PageSize, 200);
        }
    }
}
