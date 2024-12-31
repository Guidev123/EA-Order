using Orders.Application.DTOs;
using Orders.Application.Events.Vouchers;
using Orders.Core.Entities;

namespace Orders.Application.Events.Factories
{
    public static class VoucherEventFactory
    {
        public static VoucherCreatedProjectionEvent CreateVoucherCreatedProjectionEvent(Voucher voucher)
        {
            var voucherDto = new VoucherDTO(voucher.Id, voucher.Code, voucher.Percentual, voucher.DiscountValue,
                voucher.Quantity, voucher.DiscountType, voucher.ExpiresAt, voucher.CreatedAt, voucher.IsActive);

            return new(voucherDto);
        }
    }
}
