using NexusTix.Domain.Entities;

namespace NexusTix.Persistence.Repositories.Cities
{
    public interface ICityRepository : IGenericRepository<City, int>
    { 
        Task<City?> GetCityWithDistrictsAsync(int id);
        Task<IEnumerable<City>> GetCitiesWithDistrictsAsync();
        Task<City?> GetCityAggregateAsync(int id);
        Task<IEnumerable<City>> GetCitiesAggregateAsync();
    }
}
