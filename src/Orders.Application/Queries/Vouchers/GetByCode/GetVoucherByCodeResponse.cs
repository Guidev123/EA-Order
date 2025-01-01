using Orders.Core.Enums;

namespace Orders.Application.Queries.Vouchers.GetByCode
{
    public record GetVoucherByCodeResponse(
        decimal? Percentual, decimal? DiscountValue,
        string Code, EDiscountType DiscountType);
}
