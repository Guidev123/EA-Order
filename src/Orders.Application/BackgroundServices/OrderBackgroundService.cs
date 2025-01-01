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
        private void SetSubscriber() =>
            _bus.SubscribeAsync<OrderCreatedProjectionEvent>("OrderCreatedProjectionEvent", OrderProjectionAsync);

        private async Task OrderProjectionAsync(OrderCreatedProjectionEvent projectionEvent)
        {
            using var scope = _serviceProvider.CreateScope();
            var respository = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            await respository.Orders.CreateToProjectionAsync(projectionEvent.Order.MapToEntity());
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetSubscriber();
            return Task.CompletedTask;
        }
    }
}
