using EA.IntegrationEvents.Integration.ReceivedAddress;
using MediatR;
using Orders.Application.Services;
using Orders.Core.Events;
using SharedLib.MessageBus;

namespace Orders.Application.Events.Orders
{
    public class ReceivedAddressEventHandler(IMessageBus bus, IUserService user) : INotificationHandler<EventNotification<ReceivedAddressEvent>>
    {
        private readonly IMessageBus _bus = bus;
        private readonly IUserService _user = user;
        public async Task Handle(EventNotification<ReceivedAddressEvent> notification, CancellationToken cancellationToken)
        {
            var userId = await _user.GetUserIdAsync();

            await _bus.PublishAsync(new ReceivedAddressIntegrationEvent(
            userId.Value,
            notification.DomainEvent.Address.Street, notification.DomainEvent.Address.Number,
            notification.DomainEvent.Address.AdditionalInfo,
            notification.DomainEvent.Address.Neighborhood, notification.DomainEvent.Address.ZipCode,
            notification.DomainEvent.Address.City, notification.DomainEvent.Address.State)
            );
        }
    }
}
