using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Orders.Application
{
    public static class ApplicationModule
    {
        public static void AddApplication(this IServiceCollection services)
        {
            AddMediatrHandlers(services);
        }

        public static void AddMediatrHandlers(this IServiceCollection services) =>
            services.AddMediatR(x => x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}
