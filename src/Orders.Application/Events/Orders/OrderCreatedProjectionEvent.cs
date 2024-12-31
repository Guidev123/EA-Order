using Orders.Core.Entities;
using Orders.Core.Events;

namespace Orders.Application.Events.Orders
{
    public class OrderCreatedProjectionEvent(Order order) : IDomainEvent
    {
        public Order Order { get; private set; } = order;
        public Guid EventId => Guid.NewGuid();
        public DateTime OccurredAt => DateTime.Now;
    }
}
