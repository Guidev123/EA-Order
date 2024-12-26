using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orders.Core.Repositories;
using Orders.Infrastructure.Persistence.Factories;
using Orders.Infrastructure.Persistence.Repositories;

namespace Orders.Infrastructure
{
    public static class InfraModule
    {
        public static void AddInfra(this IServiceCollection services)
        {
            services.AddRepositories();
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
