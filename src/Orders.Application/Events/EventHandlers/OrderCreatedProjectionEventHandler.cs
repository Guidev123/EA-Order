﻿using MediatR;
using Orders.Application.Events.Orders;
using SharedLib.MessageBus;

namespace Orders.Application.Events.EventHandlers
{
    public sealed class OrderCreatedProjectionEventHandler(IMessageBus bus)
                      : INotificationHandler<EventNotification<OrderCreatedProjectionEvent>>
    {
        private readonly IMessageBus _bus = bus;
        public async Task Handle(EventNotification<OrderCreatedProjectionEvent> notification, CancellationToken cancellationToken)
           => await _bus.PublishAsync(notification.DomainEvent);
    }
}
