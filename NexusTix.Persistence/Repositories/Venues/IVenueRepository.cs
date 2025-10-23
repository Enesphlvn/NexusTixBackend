using NexusTix.Domain.Entities;

namespace NexusTix.Persistence.Repositories.Venues
{
    public interface IVenueRepository : IGenericRepository<Venue, int>
    {
        Task<IEnumerable<Venue>> GetVenuesByCityAsync(int cityId);
        Task<IEnumerable<Venue>> GetVenuesByDistrictAsync(int districtId);
        Task<Venue?> GetVenueWithEventsAsync(int id);
        Task<IEnumerable<Venue>> GetVenuesWithEventsAsync();
    }
}
