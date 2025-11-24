using Microsoft.EntityFrameworkCore;
using NexusTix.Domain.Entities;
using NexusTix.Persistence.Context;

namespace NexusTix.Persistence.Repositories.Tickets
{
    public class TicketRepository : GenericRepository<Ticket, int>, ITicketRepository
    {
        public TicketRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<int> GetTicketCountByEventAsync(int eventId)
        {
            return await _context.Tickets.CountAsync(x => x.EventId == eventId && !x.IsCancelled);
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByDateRangeAsync(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return await _context.Tickets.Include(x => x.Event).Include(x => x.User)
                .Where(x => x.PurchaseDate >= startDate && x.PurchaseDate <= endDate).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByEventAsync(int eventId)
        {
            return await _context.Tickets.Include(x => x.User).Where(x => x.EventId == eventId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetTicketsByUserAsync(int userId)
        {
            return await _context.Tickets
                .Include(x => x.Event).ThenInclude(x => x.Venue).ThenInclude(x => x.District).ThenInclude(x => x.City)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.PurchaseDate).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Ticket>> GetTicketsAggregateAsync()
        {
            return await _context.Tickets
                .Include(x => x.User)
                .Include(x => x.Event).ThenInclude(x => x.EventType)
                .Include(x => x.Event).ThenInclude(x => x.Venue).ThenInclude(x => x.District).ThenInclude(x => x.City)
                .AsNoTracking().ToListAsync();
        }

        public async Task<Ticket?> GetTicketAggregateAsync(int id)
        {
            return await _context.Tickets
                .Include(x => x.User)
                .Include(x => x.Event).ThenInclude(x => x.EventType)
                .Include(x => x.Event).ThenInclude(x => x.Venue).ThenInclude(x => x.District).ThenInclude(x => x.City)
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> HasUserTicketForEventAsync(int userId, int eventId)
        {
            return await _context.Tickets.AsNoTracking().AnyAsync(x => x.UserId == userId && x.EventId == eventId);
        }
    }
}
