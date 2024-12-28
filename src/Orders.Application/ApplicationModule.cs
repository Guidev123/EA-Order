using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orders.Application.BackgroundServices;
using Orders.Application.UseCases.Orders.Create;
using Orders.Application.UseCases.Vouchers.Create;
using SharedLib.MessageBus;

namespace Orders.Application
{
    public static class ApplicationModule
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration) 
        {
            AddUseCases(services);
        }

        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<ICreateOrderHandler, CreateOrderHandler>();
            services.AddTransient<ICreateVoucherHandler, CreateVoucherHandler>();
        }
    }
}
