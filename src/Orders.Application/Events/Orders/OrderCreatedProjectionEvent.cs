using Orders.Application.DTOs;
using Orders.Core.Events;

namespace Orders.Application.Events.Orders
{
    public class OrderCreatedProjectionEvent(OrderDTO order) : IDomainEvent
    {
        public OrderDTO Order { get; private set; } = order;
        public Guid EventId => Guid.NewGuid();
        public DateTime OccurredAt => DateTime.Now;
    }
}
