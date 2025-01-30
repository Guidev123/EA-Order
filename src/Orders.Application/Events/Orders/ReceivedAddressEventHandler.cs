using EA.IntegrationEvents.Integration.ReceivedAddress;
using MediatR;
using Orders.Core.Events;
using SharedLib.MessageBus;

namespace Orders.Application.Events.Orders
{
    public class ReceivedAddressEventHandler(IMessageBus bus) : INotificationHandler<EventNotification<ReceivedAddressEvent>>
    {
        private readonly IMessageBus _bus = bus;
        public async Task Handle(EventNotification<ReceivedAddressEvent> notification, CancellationToken cancellationToken)
        => await _bus.PublishAsync(new ReceivedAddressIntegrationEvent(
        notification.DomainEvent.Address.Street, notification.DomainEvent.Address.Number,
        notification.DomainEvent.Address.AdditionalInfo,
        notification.DomainEvent.Address.Neighborhood, notification.DomainEvent.Address.ZipCode,
        notification.DomainEvent.Address.City, notification.DomainEvent.Address.State)
        );
    }
}
