
namespace Orders.Core.Events
{
    public class VoucherCreatedProjectionEvent : IDomainEvent
    {
        public Guid EventId => Guid.NewGuid();
        public DateTime OccurredAt => DateTime.Now;
    }
}
