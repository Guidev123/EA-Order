using MediatR;
using Orders.Core.Events;
using SharedLib.MessageBus;

namespace Orders.Application.Events.OrderCreated
{
    public sealed class OrderCreatedEventHandler(IMessageBus bus) : INotificationHandler<DomainEventNotification<OrderCreatedProjectionEvent>>
    {
        private readonly IMessageBus _bus = bus;
        public async Task Handle(DomainEventNotification<OrderCreatedProjectionEvent> notification, CancellationToken cancellationToken)
        {
            var domainEvent = notification.DomainEvent;

            await _bus.PublishAsync(domainEvent);
        }
    }
}
