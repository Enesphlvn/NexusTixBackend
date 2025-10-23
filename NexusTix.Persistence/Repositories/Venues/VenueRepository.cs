using Microsoft.EntityFrameworkCore;
using NexusTix.Domain.Entities;
using NexusTix.Persistence.Context;

namespace NexusTix.Persistence.Repositories.Venues
{
    public class VenueRepository : GenericRepository<Venue, int>, IVenueRepository
    {
        private readonly AppDbContext _context;

        public VenueRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Venue>> GetVenuesByCityAsync(int cityId)
        {
            return await _context.Venues.Include(x => x.District).Where(x => x.District.CityId == cityId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Venue>> GetVenuesByDistrictAsync(int districtId)
        {
            return await _context.Venues.Include(x => x.District).ThenInclude(x => x.City).Where(x => x.DistrictId == districtId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Venue>> GetVenuesWithEventsAsync()
        {
            return await _context.Venues.Include(x => x.Events).AsNoTracking().ToListAsync();
        }

        public async Task<Venue?> GetVenueWithEventsAsync(int id)
        {
            return await _context.Venues.Include(x => x.Events).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
