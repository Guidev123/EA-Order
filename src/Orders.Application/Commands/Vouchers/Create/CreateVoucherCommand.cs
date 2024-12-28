using MediatR;
using Orders.Application.Responses;
using Orders.Core.Enums;

namespace Orders.Application.Commands.Vouchers.Create
{
    public record CreateVoucherCommand(
        string Code, decimal? Percentual,
        decimal? DiscountValue, int Quantity,
        EDiscountType DiscountType, DateTime ExpiresAt)
      : IRequest<Response<CreateVoucherResponse>>;
}
