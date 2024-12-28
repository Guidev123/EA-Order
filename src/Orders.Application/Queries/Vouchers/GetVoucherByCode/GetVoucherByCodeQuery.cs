using MediatR;
using Orders.Application.Responses;

namespace Orders.Application.Queries.Vouchers.GetVoucherByCode
{
    public record GetVoucherByCodeQuery(string Code) : IRequest<Response<GetVoucherByCodeResponse>>;
}
