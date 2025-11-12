using NexusTix.Domain.Entities;

namespace NexusTix.Persistence.Repositories.Events
{
    public interface IEventRepository : IGenericRepository<Event, int>
    {
        Task<Event?> GetEventAggregateAsync(int eventId);
        Task<IEnumerable<Event>> GetEventsAggregateAsync();
        Task<IEnumerable<Event>> GetEventsByEventTypeAsync(int eventTypeId);
        Task<IEnumerable<Event>> GetEventsByVenueAsync(int venueId);
        Task<IEnumerable<Event>> GetEventsWithHighestSalesAsync(int numberOfEvents);
        Task<IEnumerable<Event>> GetEventsByDateRangeAsync(DateTimeOffset startDate, DateTimeOffset endDate);
        Task<IEnumerable<Event>> GetEventsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<IEnumerable<Event>> GetEventsByUserTicketsAsync(int userId);
        Task<IEnumerable<Event>> GetFilteredEventsAsync(int? cityId, int? eventTypeId, DateTimeOffset? date);
    }
}