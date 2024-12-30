using MediatR;
using Orders.Core.Events;
using SharedLib.MessageBus;

namespace Orders.Application.Events.EventHandlers
{
    public sealed class OrderCreatedEventHandler(IMessageBus bus)
                      : INotificationHandler<DomainEventNotification<OrderCreatedEvent>>
    {
        private readonly IMessageBus _bus = bus;
        public async Task Handle(DomainEventNotification<OrderCreatedEvent> notification, CancellationToken cancellationToken)
            => await _bus.PublishAsync(notification.DomainEvent);
    }
}
