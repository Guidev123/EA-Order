
namespace Orders.Core.Events
{
    public record OrderCreatedEvent(Guid CustomerId) : IDomainEvent
    {
        public Guid EventId => Guid.NewGuid();
        public DateTime OccurredAt => DateTime.Now;
    }
}
