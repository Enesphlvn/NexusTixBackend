using Microsoft.EntityFrameworkCore;
using NexusTix.Domain.Entities;
using NexusTix.Persistence.Context;

namespace NexusTix.Persistence.Repositories.Districts
{
    public class DistrictRepository : GenericRepository<District, int>, IDistrictRepository
    {
        private readonly AppDbContext _context;

        public DistrictRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<District?> GetDistrictAggregateAsync(int id)
        {
            return await _context.Districts
                .Include(x => x.City)
                .Include(x => x.Venues).ThenInclude(x => x.Events).ThenInclude(x => x.EventType)
                .Include(x => x.Venues).ThenInclude(x => x.Events).ThenInclude(x => x.Tickets).ThenInclude(x => x.User)
                .AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<District>> GetDistrictsAggregateAsync()
        {
            return await _context.Districts
                .Include(x => x.City)
                .Include(x => x.Venues).ThenInclude(x => x.Events).ThenInclude(x => x.EventType)
                .Include(x => x.Venues).ThenInclude(x => x.Events).ThenInclude(x => x.Tickets).ThenInclude(x => x.User)
                .AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<District>> GetDistrictsWithVenuesAsync()
        {
            return await _context.Districts.Include(x => x.Venues).AsNoTracking().ToListAsync();
        }

        public async Task<District?> GetDistrictWithVenuesAsync(int id)
        {
            return await _context.Districts.Include(x => x.Venues).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
