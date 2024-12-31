using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Orders.Application.Services;
using Orders.Core.Repositories;
using Orders.Infrastructure.Persistence.Contexts;
using Orders.Infrastructure.Persistence.Factories;
using Orders.Infrastructure.Persistence.Mappings;
using Orders.Infrastructure.Persistence.Repositories;
using Orders.Infrastructure.Services;

namespace Orders.Infrastructure
{
    public static class InfraModule
    {
        public static void AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRepositories();
            services.AddServices();
            services.AddMongo(configuration);
        }
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }

        public static void AddMongo(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ReadDbContext>();

            services.AddSingleton<IMongoClient>(sp =>
            {
                var connectionString = configuration.GetConnectionString("ReadDbContext");
                return new MongoClient(connectionString);
            });

            services.AddScoped(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase("EA-Order");
            });

            MongoDbMappings.MapEntity();
            MongoDbMappings.MapVoucher();
            MongoDbMappings.MapOrder();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var connectionString = configuration.GetConnectionString("WriteDbContext")
                ?? throw new ArgumentNullException("The connection string 'WriteDbContext' is not configured");
                return new SqlConnectionFactory(connectionString);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IVoucherRepository, VoucherRepository>();
        }
    }
}
