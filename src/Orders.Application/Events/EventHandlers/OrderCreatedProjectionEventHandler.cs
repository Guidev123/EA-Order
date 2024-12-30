using MediatR;
using Orders.Core.Events;
using SharedLib.MessageBus;

namespace Orders.Application.Events.EventHandlers
{
    public sealed class OrderCreatedProjectionEventHandler(IMessageBus bus)
                      : INotificationHandler<DomainEventNotification<OrderCreatedProjectionEvent>>
    {
        private readonly IMessageBus _bus = bus;
        public async Task Handle(DomainEventNotification<OrderCreatedProjectionEvent> notification, CancellationToken cancellationToken)
           => await _bus.PublishAsync(notification.DomainEvent);
    }
}
