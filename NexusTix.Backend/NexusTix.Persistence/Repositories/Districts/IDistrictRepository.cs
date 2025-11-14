using NexusTix.Domain.Entities;

namespace NexusTix.Persistence.Repositories.Districts
{
    public interface IDistrictRepository : IGenericRepository<District, int>
    {
        Task<District?> GetDistrictWithVenuesAsync(int id);
        Task<IEnumerable<District>> GetDistrictsWithVenuesAsync();
        Task<District?> GetDistrictAggregateAsync(int id);
        Task<IEnumerable<District>> GetDistrictsAggregateAsync();
        Task<IEnumerable<District>> GetDistrictsByCityAsync(int cityId);
    }
}
