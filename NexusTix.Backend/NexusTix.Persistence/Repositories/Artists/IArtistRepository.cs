using NexusTix.Domain.Entities;

namespace NexusTix.Persistence.Repositories.Artists
{
    public interface IArtistRepository : IGenericRepository<Artist, int>
    {
        Task<Artist?> GetArtistWithEventsAsync(int id);
        Task<IEnumerable<Artist>> GetArtistsWithEventsAsync();
        Task<List<Artist>> GetArtistsByIdsAsync(IEnumerable<int> ids);
        Task<IEnumerable<Artist>> GetAllArtistsForAdminAsync();
        Task<Artist?> GetArtistForAdminAsync(int id);
        Task<Artist?> GetByIdIncludingPassiveAsync(int id);
    }
}
