using MediatR;
using SharedLib.MessageBus;

namespace Orders.Application.Events.OrderCreated
{
    public sealed class OrderCreatedHandler(IMessageBus bus) : INotificationHandler<OrderCreatedEvent>
    {
        private readonly IMessageBus _bus = bus;
        public async Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
            => await _bus.PublishAsync(notification);
    }
}
