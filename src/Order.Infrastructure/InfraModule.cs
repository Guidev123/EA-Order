using Microsoft.Extensions.Configuration;
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
            services.AddScoped<IUnitOfWork>(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                return connectionString is not null
                ? new UnitOfWork(connectionString)
                : throw new ArgumentNullException("The connection string 'DefaultConnection' is not configured");
            });
        }
    }
}
