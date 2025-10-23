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
