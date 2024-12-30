using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orders.Application.BackgroundServices;
using SharedLib.MessageBus;
using System.Reflection;

namespace Orders.Application
{
    public static class ApplicationModule
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddMediatrHandlers(services);
            RegisterBackgroundService(services, configuration);
        }

        public static void AddMediatrHandlers(this IServiceCollection services) =>
            services.AddMediatR(x => x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        public static void RegisterBackgroundService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"));
            services.AddHostedService<OrderBackgroundService>();
        }
    }
}
