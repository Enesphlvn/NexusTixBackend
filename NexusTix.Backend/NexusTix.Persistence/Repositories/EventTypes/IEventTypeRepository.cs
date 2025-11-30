using NexusTix.Domain.Entities;

namespace NexusTix.Persistence.Repositories.EventTypes
{
    public interface IEventTypeRepository : IGenericRepository<EventType, int>
    {
        Task<EventType?> GetEventTypeWithEventsAsync(int id);
        Task<IEnumerable<EventType>> GetEventTypesWithEventsAsync();
        Task<EventType?> GetEventTypeAggregateAsync(int id);
        Task<IEnumerable<EventType>> GetEventTypesAggregateAsync();
        Task<IEnumerable<EventType>> GetEventTypesByArtistAsync(int artistId);
    }
}
