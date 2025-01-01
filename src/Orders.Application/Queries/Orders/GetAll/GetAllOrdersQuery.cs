using MediatR;
using Orders.Application.Responses;

namespace Orders.Application.Queries.Orders.GetAll
{
    public record GetAllOrdersQuery(int PageNumber, int PageSize, Guid CustomerId)
                : IRequest<PagedResponse<List<GetAllOrdersResponse>>>;
}
