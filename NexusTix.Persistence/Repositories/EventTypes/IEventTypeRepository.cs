using NexusTix.Domain.Entities;

namespace NexusTix.Persistence.Repositories.EventTypes
{
    public interface IEventTypeRepository : IGenericRepository<EventType, int>
    {
        Task<EventType?> GetEventTypeWithEventsAsync(int id);
        Task<IEnumerable<EventType>> GetEventTypesWithEventsAsync();
    }
}
