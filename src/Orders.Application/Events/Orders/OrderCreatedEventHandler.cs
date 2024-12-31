using MediatR;
using Orders.Core.Events;
using SharedLib.MessageBus;

namespace Orders.Application.Events.Orders
{
    public sealed class OrderCreatedEventHandler(IMessageBus bus)
                      : INotificationHandler<EventNotification<OrderCreatedEvent>>
    {
        private readonly IMessageBus _bus = bus;
        public async Task Handle(EventNotification<OrderCreatedEvent> notification, CancellationToken cancellationToken)
            => await _bus.PublishAsync(notification.DomainEvent);
    }
}
