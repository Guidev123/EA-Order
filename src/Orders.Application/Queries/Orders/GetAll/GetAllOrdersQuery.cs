using MediatR;
using Orders.Application.Responses;

namespace Orders.Application.Queries.Orders.GetAll
{
    public record GetAllOrdersQuery(int PageNumber, int PageSize) : IRequest<PagedResponse<List<GetAllOrdersResponse>>>;
}
