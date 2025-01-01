using Orders.Application.Commands.Vouchers.Create;
using Orders.Application.DTOs;
using Orders.Core.Entities;

namespace Orders.Application.Mappers
{
    public static class VoucherMapper
    {
        public static Voucher MapToEntity(this CreateVoucherCommand command) =>
            new(command.Code, command.Percentual, command.DiscountValue, command.Quantity, command.DiscountType, command.ExpiresAt);
        public static Voucher MapToEntity(this VoucherDTO command) =>
            new(command.Id, command.Code, command.Percentual, command.DiscountValue,
                command.Quantity, command.DiscountType, command.ExpiresAt, command.CreatedAt);

        public static VoucherDTO MapFromEntity(this Voucher command) =>
            new(command.Id, command.Code, command.Percentual, command.DiscountValue,
                command.Quantity, command.DiscountType, command.ExpiresAt, command.CreatedAt, command.IsActive);
    }
}
