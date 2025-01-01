using MediatR;
using Orders.Application.Responses;

namespace Orders.Application.Queries.Vouchers.GetByCode
{
    public record GetVoucherByCodeQuery(string Code) : IRequest<Response<GetVoucherByCodeResponse>>;
}
