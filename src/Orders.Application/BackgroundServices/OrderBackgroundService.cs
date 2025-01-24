using EA.IntegrationEvents.Integration.PaymentConfirmed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orders.Application.Events.Orders;
using Orders.Application.Mappers;
using Orders.Core.Repositories;
using SharedLib.MessageBus;

namespace Orders.Application.BackgroundServices
{
    public sealed class OrderBackgroundService(IMessageBus bus, IServiceProvider serviceProvider)
                      : BackgroundService
    {
        private readonly IMessageBus _bus = bus;
        private readonly IServiceProvider _serviceProvider = serviceProvider;
        private void SetSubscriber()
        {
            _bus.SubscribeAsync<OrderCreatedProjectionEvent>("OrderCreatedProjectionEvent", OrderProjectionAsync);
            _bus.SubscribeAsync<PaymentConfirmedIntegrationEvent>("PaymentConfirmedEvent", OrderPaidAsync);
            _bus.SubscribeAsync<OrderPaidProjectionEvent>("OrderPaidProjectionEvent", OrderPaidProjectionAsync);
        }

        private async Task OrderProjectionAsync(OrderCreatedProjectionEvent projectionEvent)
        {
            using var scope = _serviceProvider.CreateScope();
            var respository = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            await respository.Orders.CreateToProjectionAsync(projectionEvent.Order.MapToEntity());
        }

        private async Task OrderPaidAsync(PaymentConfirmedIntegrationEvent paymentConfirmedEvent)
        {
            using var scope = _serviceProvider.CreateScope();
            var respository = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var order = await respository.Orders.GetByCodeAsync(paymentConfirmedEvent.OrderCode);
            if (order is null) return;
            order.PayOrder();

            await respository.Orders.UpdateAsync(order);
            order.AddEvent(new OrderPaidProjectionEvent(paymentConfirmedEvent.OrderCode));

            await respository.PublishDomainEventsAsync(order);
        }

        private async Task OrderPaidProjectionAsync(OrderPaidProjectionEvent projectionEvent)
        {
            using var scope = _serviceProvider.CreateScope();
            var respository = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var order = await respository.Orders.GetByCodeAsync(projectionEvent.OrderCode);
            if(order is null) return;
            order.PayOrder();

            await respository.Orders.UpdateToProjectionAsync(order);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetSubscriber();
            return Task.CompletedTask;
        }
    }
}
