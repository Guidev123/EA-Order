using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orders.Application.Services;
using Orders.Core.Repositories;
using Orders.Infrastructure.Persistence.Contexts;
using Orders.Infrastructure.Persistence.Factories;
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
            services.AddReadDbContext(configuration);
        }
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
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

        public static void AddReadDbContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<ReadDbContext>(opt =>
            opt.UseMongoDB(configuration.GetConnectionString("ReadDbContext") ?? string.Empty, "EA-Orders"));
    }
}
