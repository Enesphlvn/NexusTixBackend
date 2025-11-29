using NexusTix.Domain.Entities;

namespace NexusTix.Persistence.Repositories.Venues
{
    public interface IVenueRepository : IGenericRepository<Venue, int>
    {
        Task<Venue?> GetVenueWithEventsAsync(int id);
        Task<IEnumerable<Venue>> GetVenuesWithEventsAsync();
        Task<Venue?> GetVenueAggregateAsync(int id);
        Task<IEnumerable<Venue>> GetVenuesAggregateAsync();
        Task<Venue?> GetVenueForAdminAsync(int id);
        Task<IEnumerable<Venue>> GetAllVenuesForAdminAsync();
    }
}
