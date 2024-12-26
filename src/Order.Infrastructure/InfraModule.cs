using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Core.Repositories;
using Order.Infrastructure.Persistence.Factories;
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
            services.AddSingleton(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new ArgumentNullException("The connection string 'DefaultConnection' is not configured");
                return new SqlConnectionFactory(connectionString);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
