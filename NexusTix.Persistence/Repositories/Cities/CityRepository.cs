using Microsoft.EntityFrameworkCore;
using NexusTix.Domain.Entities;
using NexusTix.Persistence.Context;

namespace NexusTix.Persistence.Repositories.Cities
{
    public class CityRepository : GenericRepository<City, int>, ICityRepository
    {
        public CityRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<City>> GetCitiesAggregateAsync()
        {
            return await _context.Cities
                .Include(x => x.Districts).ThenInclude(x => x.Venues).ThenInclude(x => x.Events).ThenInclude(x => x.EventType)
                .Include(x => x.Districts).ThenInclude(x => x.Venues).ThenInclude(x => x.Events).ThenInclude(x => x.Tickets).ThenInclude(x => x.User)
                .AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<City>> GetCitiesWithDistrictsAsync()
        {
            return await _context.Cities.Include(x => x.Districts).AsNoTracking().ToListAsync();
        }

        public async Task<City?> GetCityAggregateAsync(int id)
        {
            return await _context.Cities
                .Include(x => x.Districts).ThenInclude(x => x.Venues).ThenInclude(x => x.Events).ThenInclude(x => x.EventType)
                .Include(x => x.Districts).ThenInclude(x => x.Venues).ThenInclude(x => x.Events).ThenInclude(x => x.Tickets).ThenInclude(x => x.User)
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<City?> GetCityWithDistrictsAsync(int id)
        {
            return await _context.Cities.Include(x => x.Districts).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
