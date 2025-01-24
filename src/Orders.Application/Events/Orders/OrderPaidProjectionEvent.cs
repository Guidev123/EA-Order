using Orders.Core.Events;

namespace Orders.Application.Events.Orders;

public class OrderPaidProjectionEvent : IDomainEvent
{
    public Guid EventId => Guid.NewGuid();
    public DateTime OccurredAt => DateTime.Now;
    public string OrderCode { get; private set; }

    public OrderPaidProjectionEvent(string orderCode)
    {
        OrderCode = orderCode;
    }
}
