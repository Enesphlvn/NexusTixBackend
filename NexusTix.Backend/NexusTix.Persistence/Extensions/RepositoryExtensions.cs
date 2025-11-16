using Microsoft.Extensions.DependencyInjection;
using NexusTix.Persistence.Repositories;
using NexusTix.Persistence.Repositories.Dashboards;

namespace NexusTix.Persistence.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDashboardRepository, DashboardRepository>();

            return services;
        }
    }
}
