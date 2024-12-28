using Orders.Application.Commands.Vouchers.Create;
using Orders.Core.Entities;

namespace Orders.Application.Mappers
{
    public static class VoucherMapper
    {
        public static Voucher MapToEntity(this CreateVoucherCommand command) =>
            new(command.Code, command.Percentual, command.DiscountValue, command.Quantity, command.DiscountType, command.ExpiresAt);
    }
}
