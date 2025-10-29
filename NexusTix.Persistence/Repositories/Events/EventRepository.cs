﻿using Microsoft.EntityFrameworkCore;
using NexusTix.Domain.Entities;
using NexusTix.Persistence.Context;

namespace NexusTix.Persistence.Repositories.Events
{
    public class EventRepository : GenericRepository<Event, int>, IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context) : base(context)
        {
            _context = context;
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
                .Include(x => x.Tickets).ThenInclude(x => x.User).AsNoTracking().ToListAsync();
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
                .Include(x => x.Tickets).ThenInclude(x => x.User).AsNoTracking().FirstOrDefaultAsync(x => x.Id == eventId);
        }
    }
}
