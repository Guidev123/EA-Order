using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orders.Application.Events.Vouchers;
using Orders.Application.Mappers;
using Orders.Core.Repositories;
using SharedLib.MessageBus;

namespace Orders.Application.BackgroundServices
{
    public sealed class VoucherBackgroundService(IMessageBus bus, IServiceProvider serviceProvider)
                      : BackgroundService
    {
        private readonly IMessageBus _bus = bus;
        private readonly IServiceProvider _serviceProvider = serviceProvider;

        private void SetSubscribers()
        {
            _bus.SubscribeAsync<VoucherCreatedProjectionEvent>("VoucherCreatedProjectionEvent", CreatedVoucherProjectionAsync);
            _bus.SubscribeAsync<VoucherUpdatedProjectionEvent>("VoucherUpdatedProjectionEvent", UpdatedVoucherProjectionAsync);
        }

        private async Task CreatedVoucherProjectionAsync(VoucherCreatedProjectionEvent projectionEvent)
        {
            using var scope = _serviceProvider.CreateScope();
            var respository = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            await respository.Vouchers.CreateToProjectionAsync(projectionEvent.Voucher.MapToEntity());
        }

        private async Task UpdatedVoucherProjectionAsync(VoucherUpdatedProjectionEvent projectionEvent)
        {
            using var scope = _serviceProvider.CreateScope();
            var respository = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            await respository.Vouchers.UpdateToProjectionAsync(projectionEvent.Voucher.MapToEntity());
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetSubscribers();
            return Task.CompletedTask;
        }
    }
}
