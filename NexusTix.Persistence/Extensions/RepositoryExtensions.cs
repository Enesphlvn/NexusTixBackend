using Microsoft.Extensions.DependencyInjection;
using NexusTix.Persistence.Repositories;
using NexusTix.Persistence.Repositories.Cities;
using NexusTix.Persistence.Repositories.Districts;
using NexusTix.Persistence.Repositories.Events;
using NexusTix.Persistence.Repositories.EventTypes;
using NexusTix.Persistence.Repositories.Tickets;
using NexusTix.Persistence.Repositories.Users;
using NexusTix.Persistence.Repositories.Venues;

namespace NexusTix.Persistence.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IDistrictRepository, DistrictRepository>();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IEventTypeRepository, EventTypeRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<IVenueRepository, VenueRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
