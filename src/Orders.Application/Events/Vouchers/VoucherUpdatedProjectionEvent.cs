using Orders.Application.DTOs;
using Orders.Core.Events;

namespace Orders.Application.Events.Vouchers
{
    public class VoucherUpdatedProjectionEvent(VoucherDTO voucher) : IDomainEvent
    {
        public VoucherDTO Voucher { get; private set; } = voucher;
        public Guid EventId => Guid.NewGuid();
        public DateTime OccurredAt => DateTime.Now;
    }
}
