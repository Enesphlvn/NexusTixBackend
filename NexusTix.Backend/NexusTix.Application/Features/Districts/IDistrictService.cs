using NexusTix.Application.Features.Districts.Create;
using NexusTix.Application.Features.Districts.Responses;
using NexusTix.Application.Features.Districts.Update;

namespace NexusTix.Application.Features.Districts
{
    public interface IDistrictService
    {
        Task<ServiceResult<IEnumerable<DistrictResponse>>> GetAllDistrictsAsync();
        Task<ServiceResult<IEnumerable<DistrictResponse>>> GetPagedAllDistrictsAsync(int pageNumber, int pageSize);
        Task<ServiceResult<DistrictResponse>> GetByIdAsync(int id);

        Task<ServiceResult<DistrictWithVenuesResponse>> GetDistrictWithVenuesAsync(int id);
        Task<ServiceResult<IEnumerable<DistrictWithVenuesResponse>>> GetDistrictsWithVenuesAsync();
        Task<ServiceResult<DistrictAggregateResponse>> GetDistrictAggregateAsync(int id);
        Task<ServiceResult<IEnumerable<DistrictAggregateResponse>>> GetDistrictsAggregateAsync();
        Task<ServiceResult<IEnumerable<DistrictResponse>>> GetDistrictsByCityAsync(int cityId);

        Task<ServiceResult<DistrictResponse>> CreateAsync(CreateDistrictRequest request);
        Task<ServiceResult> UpdateAsync(UpdateDistrictRequest request);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult> PassiveAsync(int id);
    }
}
