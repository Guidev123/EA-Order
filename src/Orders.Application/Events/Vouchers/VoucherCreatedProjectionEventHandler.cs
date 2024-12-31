using MediatR;
using SharedLib.MessageBus;

namespace Orders.Application.Events.Vouchers
{
    public sealed class VoucherCreatedProjectionEventHandler(IMessageBus bus)
                      : INotificationHandler<EventNotification<VoucherCreatedProjectionEvent>>
    {
        private readonly IMessageBus _bus = bus;
        public async Task Handle(EventNotification<VoucherCreatedProjectionEvent> notification, CancellationToken cancellationToken)
            => await _bus.PublishAsync(notification.DomainEvent);
    }
}
