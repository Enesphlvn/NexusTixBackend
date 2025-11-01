using Microsoft.Extensions.DependencyInjection;
using NexusTix.Persistence.Repositories;

namespace NexusTix.Persistence.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
