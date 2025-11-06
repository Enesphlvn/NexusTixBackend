using NexusTix.Application.Features.Cities.Create;
using NexusTix.Application.Features.Cities.Responses;
using NexusTix.Application.Features.Cities.Update;

namespace NexusTix.Application.Features.Cities
{
    public interface ICityService
    {
        Task<ServiceResult<IEnumerable<CityResponse>>> GetAllCitiesAsync();
        Task<ServiceResult<IEnumerable<CityResponse>>> GetPagedAllCitiesAsync(int pageNumber, int pageSize);
        Task<ServiceResult<CityResponse>> GetByIdAsync(int id);

        Task<ServiceResult<CityWithDistrictsResponse>> GetCityWithDistrictsAsync(int id);
        Task<ServiceResult<IEnumerable<CityWithDistrictsResponse>>> GetCitiesWithDistrictsAsync();
        Task<ServiceResult<CityAggregateResponse>> GetCityAggregateAsync(int id);
        Task<ServiceResult<IEnumerable<CityAggregateResponse>>> GetCitiesAggregateAsync();

        Task<ServiceResult<CityResponse>> CreateAsync(CreateCityRequest request);
        Task<ServiceResult> UpdateAsync(UpdateCityRequest request);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult> PassiveAsync(int id);
    }
}
