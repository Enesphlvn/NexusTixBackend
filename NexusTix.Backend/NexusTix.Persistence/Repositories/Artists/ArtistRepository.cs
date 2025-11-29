using Microsoft.EntityFrameworkCore;
using NexusTix.Domain.Entities;
using NexusTix.Persistence.Context;

namespace NexusTix.Persistence.Repositories.Artists
{
    public class ArtistRepository : GenericRepository<Artist, int>, IArtistRepository
    {
        public ArtistRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Artist>> GetAllArtistsForAdminAsync()
        {
            return await _context.Artists
                .Include(x => x.Events).ThenInclude(x => x.Venue)
                .IgnoreQueryFilters()
                .OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }

        public override async Task<IEnumerable<Artist>> GetAllAsync()
        {
            return await _context.Artists.OrderBy(x => x.Name).AsNoTracking().ToListAsync();
        }

        public async Task<Artist?> GetArtistForAdminAsync(int id)
        {
            return await _context.Artists
            .Include(x => x.Events).ThenInclude(x => x.Venue)
            .IgnoreQueryFilters()
            .OrderByDescending(x => x.Id).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Artist>> GetArtistsByIdsAsync(IEnumerable<int> ids)
        {
            return await _context.Artists.Where(a => ids.Contains(a.Id)).ToListAsync();
        }

        public async Task<IEnumerable<Artist>> GetArtistsWithEventsAsync()
        {
            return await _context.Artists
                .Include(x => x.Events).AsNoTracking().ToListAsync();
        }

        public async Task<Artist?> GetArtistWithEventsAsync(int id)
        {
            return await _context.Artists
                .Include(x => x.Events).ThenInclude(x => x.Venue).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
