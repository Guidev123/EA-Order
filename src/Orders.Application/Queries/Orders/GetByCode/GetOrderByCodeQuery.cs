using MediatR;
using Orders.Application.Responses;

namespace Orders.Application.Queries.Orders.GetByCode
{
    public record GetOrderByCodeQuery(string Code) : IRequest<Response<GetOrderByCodeResponse>>;
}
