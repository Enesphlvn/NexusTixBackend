using Microsoft.EntityFrameworkCore;
using NexusTix.Domain.Entities;
using NexusTix.Persistence.Context;

namespace NexusTix.Persistence.Repositories.EventTypes
{
    public class EventTypeRepository : GenericRepository<EventType, int>, IEventTypeRepository
    {
        private readonly AppDbContext _context;

        public EventTypeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<EventType?> GetEventTypeAggregateAsync(int id)
        {
            return await _context.EventTypes
                .Include(x => x.Events).ThenInclude(x => x.Venue).ThenInclude(x => x.District).ThenInclude(x => x.City)
                .Include(x => x.Events).ThenInclude(x => x.Tickets).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<EventType>> GetEventTypesAggregateAsync()
        {
            return await _context.EventTypes
                .Include(x => x.Events).ThenInclude(x => x.Venue).ThenInclude(x => x.District).ThenInclude(x => x.City)
                .Include(x => x.Events).ThenInclude(x => x.Tickets).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<EventType>> GetEventTypesWithEventsAsync()
        {
            return await _context.EventTypes.Include(x => x.Events).AsNoTracking().ToListAsync();
        }

        public async Task<EventType?> GetEventTypeWithEventsAsync(int id)
        {
            return await _context.EventTypes.Include(x => x.Events).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
