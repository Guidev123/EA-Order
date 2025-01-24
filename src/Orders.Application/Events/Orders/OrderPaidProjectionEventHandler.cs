using MediatR;
using SharedLib.MessageBus;

namespace Orders.Application.Events.Orders;

public sealed class OrderPaidProjectionEventHandler(IMessageBus bus)
                    : INotificationHandler<EventNotification<OrderPaidProjectionEvent>>
{
    private readonly IMessageBus _bus = bus;
    public async Task Handle(EventNotification<OrderPaidProjectionEvent> notification, CancellationToken cancellationToken)
        => await _bus.PublishAsync(notification.DomainEvent);
}
