using Orders.Core.Enums;

namespace Orders.Application.DTOs
{
    public record VoucherDTO(
        Guid Id, string Code, decimal? Percentual,
        decimal? DiscountValue, int? Quantity,
        EDiscountType? DiscountType, DateTime ExpiresAt, DateTime CreatedAt, bool IsActive);
}
