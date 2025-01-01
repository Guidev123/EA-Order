using MediatR;
using SharedLib.MessageBus;

namespace Orders.Application.Events.Vouchers
{
    public sealed class VoucherUpdatedProjectionEventHandler(IMessageBus bus)
                      : INotificationHandler<EventNotification<VoucherUpdatedProjectionEvent>>
    {
        private readonly IMessageBus _bus = bus;
        public async Task Handle(EventNotification<VoucherUpdatedProjectionEvent> notification, CancellationToken cancellationToken)
            => await _bus.PublishAsync(notification.DomainEvent);
    }
}
