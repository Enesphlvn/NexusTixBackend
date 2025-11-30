using Microsoft.EntityFrameworkCore;
using NexusTix.Domain.Entities;
using NexusTix.Persistence.Context;

namespace NexusTix.Persistence.Repositories.Venues
{  
    public class VenueRepository : GenericRepository<Venue, int>, IVenueRepository
    {
        public VenueRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Venue?> GetVenueAggregateAsync(int id)
        {
            return await _context.Venues
                .Include(x => x.District).ThenInclude(x => x.City)
                .Include(x => x.Events).ThenInclude(x => x.EventType)
                .Include(x => x.Events).ThenInclude(x => x.Tickets).ThenInclude(x => x.User)
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Venue>> GetAllVenuesForAdminAsync()
        {
            return await _context.Venues
            .Include(v => v.District).ThenInclude(x => x.City)
            .IgnoreQueryFilters().OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Venue>> GetVenuesAggregateAsync()
        {
            return await _context.Venues
                .Include(x => x.District).ThenInclude(x => x.City)
                .Include(x => x.Events).ThenInclude(x => x.EventType)
                .Include(x => x.Events).ThenInclude(x => x.Tickets).ThenInclude(x => x.User)
                .AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Venue>> GetVenuesWithEventsAsync()
        {
            return await _context.Venues.Include(x => x.Events).AsNoTracking().ToListAsync();
        }

        public async Task<Venue?> GetVenueWithEventsAsync(int id)
        {
            return await _context.Venues.Include(x => x.Events).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Venue?> GetVenueForAdminAsync(int id)
        {
            return await _context.Venues
            .Include(v => v.District).ThenInclude(x => x.City)
            .IgnoreQueryFilters().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Venue?> GetByIdIncludingPassiveAsync(int id)
        {
            return await _context.Venues.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
