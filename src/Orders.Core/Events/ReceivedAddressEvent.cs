
using Orders.Core.ValueObjects;

namespace Orders.Core.Events
{
    public class ReceivedAddressEvent : IDomainEvent
    {
        public Guid EventId => Guid.NewGuid();
        public DateTime OccurredAt => DateTime.Now;
        public Address Address { get; private set; }

        public ReceivedAddressEvent(Address address) => Address = address;
    }
}
