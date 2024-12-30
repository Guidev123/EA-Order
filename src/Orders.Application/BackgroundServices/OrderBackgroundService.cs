using Microsoft.Extensions.Hosting;

namespace Orders.Application.BackgroundServices
{
    public sealed class OrderBackgroundService : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }
    }
}
