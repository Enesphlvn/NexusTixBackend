using Microsoft.EntityFrameworkCore;
using NexusTix.Domain.Entities;
using NexusTix.Persistence.Context;

namespace NexusTix.Persistence.Repositories.Events
{
    public class EventRepository : GenericRepository<Event, int>, IEventRepository
    {
        public EventRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Event>> GetEventsByDateRangeAsync(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            return await _context.Events.Where(x => x.Date >= startDate && x.Date <= endDate).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetEventsByEventTypeAsync(int eventTypeId)
        {
            return await _context.Events.Include(x => x.EventType).Where(x => x.EventTypeId == eventTypeId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetEventsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            return await _context.Events.Where(x => x.Price >= minPrice && x.Price <= maxPrice).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetEventsByUserTicketsAsync(int userId)
        {
            return await _context.Events.Include(x => x.Tickets).AsNoTracking().Where(x => x.Tickets.Any(x => x.UserId == userId)).ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetEventsByVenueAsync(int venueId)
        {
            return await _context.Events.Include(x => x.Venue).Where(x => x.VenueId == venueId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetEventsAggregateAsync()
        {
            return await _context.Events
                .Include(x => x.EventType)
                .Include(x => x.Venue).ThenInclude(x => x.District).ThenInclude(x => x.City)
                .Include(x => x.Artists).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetEventsWithHighestSalesAsync(int numberOfEvents)
        {
            return await _context.Events.AsNoTracking().OrderByDescending(x => x.Tickets.Count())
                .Take(numberOfEvents).ToListAsync();
        }

        public async Task<Event?> GetEventAggregateAsync(int eventId)
        {
            return await _context.Events
                .Include(x => x.EventType)
                .Include(x => x.Venue).ThenInclude(x => x.District).ThenInclude(x => x.City)
                .Include(x => x.Artists).AsNoTracking().FirstOrDefaultAsync(x => x.Id == eventId);
        }

        public async Task<IEnumerable<Event>> GetFilteredEventsAsync(int? cityId, int? districtId, int? eventTypeId, int? artistId, DateTimeOffset? date)
        {
            var query = _context.Events
                .Include(x => x.EventType)
                .Include(x => x.Venue).ThenInclude(x => x.District).ThenInclude(x => x.City)
                .Include(x => x.Artists).AsNoTracking();

            if (cityId.HasValue)
            {
                query = query.Where(x => x.Venue.District.CityId == cityId.Value);
            }

            if (districtId.HasValue)
            {
                query = query.Where(x => x.Venue.DistrictId == districtId.Value);
            }

            if (eventTypeId.HasValue)
            {
                query = query.Where(x => x.EventTypeId == eventTypeId.Value);
            }

            if (artistId.HasValue)
            {
                query = query.Where(x => x.Artists.Any(a => a.Id == artistId.Value));
            }

            if (date.HasValue)
            {
                query = query.Where(x => x.Date.Date == date.Value.Date);
            }
            else
            {
                query = query.Where(x => x.Date > DateTimeOffset.UtcNow);
            }

            query = query.OrderBy(x => x.Date);

            return await query.ToListAsync();
        }

        public async Task<Event?> GetByIdWithArtistsAsync(int eventId)
        {
            return await _context.Events.Include(x => x.Artists).FirstOrDefaultAsync(x => x.Id == eventId);
        }
    }
}
