using Microsoft.Extensions.DependencyInjection;
using Order.Core.Repositories.Order.Core.Repositories;
using Order.Infrastructure.Persistence.Repositories;

namespace Order.Infrastructure
{
    public static class InfraModule
    {
        public static void AddInfra(this IServiceCollection services)
        {
            AddRepositories(services);
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
