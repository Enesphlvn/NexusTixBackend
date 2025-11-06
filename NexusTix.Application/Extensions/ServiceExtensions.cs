using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NexusTix.Application.Common.BaseRules;
using NexusTix.Application.Common.Security;
using NexusTix.Application.Features.Auth;
using NexusTix.Application.Features.Auth.Rules;
using NexusTix.Application.Features.Cities;
using NexusTix.Application.Features.Cities.Rules;
using NexusTix.Application.Features.Districts;
using NexusTix.Application.Features.Districts.Rules;
using NexusTix.Application.Features.Events;
using NexusTix.Application.Features.Events.Rules;
using NexusTix.Application.Features.EventTypes;
using NexusTix.Application.Features.EventTypes.Rules;
using NexusTix.Application.Features.Tickets;
using NexusTix.Application.Features.Tickets.Rules;
using NexusTix.Application.Features.Users;
using NexusTix.Application.Features.Users.Rules;
using NexusTix.Application.Features.Venues;
using NexusTix.Application.Features.Venues.Rules;
using System.Reflection;

namespace NexusTix.Application.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddAutoMapper(assembly);

            services.AddScoped<IAuthBusinessRules, AuthBusinessRules>();
            services.AddScoped<IBaseBusinessRules, BaseBusinessRules>();
            services.AddScoped<ICityBusinessRules, CityBusinessRules>();
            services.AddScoped<IDistrictBusinessRules, DistrictBusinessRules>();
            services.AddScoped<IEventBusinessRules, EventBusinessRules>();
            services.AddScoped<IEventTypeBusinessRules, EventTypeBusinessRules>();
            services.AddScoped<ITicketBusinessRules, TicketBusinessRules>();
            services.AddScoped<IUserBusinessRules, UserBusinessRules>();
            services.AddScoped<IVenueBusinessRules, VenueBusinessRules>();

            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IDistrictService, DistrictService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IEventTypeService, EventTypeService>();
            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IVenueService, VenueService>();

            return services;
        }
    }
}
