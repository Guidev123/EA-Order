using MediatR;
using Orders.Core.Events;

namespace Orders.Application.Events
{
    public sealed class DomainEventNotification<TDomainEvent>(TDomainEvent domainEvent) : INotification
        where TDomainEvent : IDomainEvent
    {
        public TDomainEvent DomainEvent { get; } = domainEvent;
    }
}
