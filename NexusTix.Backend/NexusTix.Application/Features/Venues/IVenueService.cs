using NexusTix.Application.Features.Venues.Create;
using NexusTix.Application.Features.Venues.Responses;
using NexusTix.Application.Features.Venues.Update;

namespace NexusTix.Application.Features.Venues
{
    public interface IVenueService
    {
        Task<ServiceResult<IEnumerable<VenueResponse>>> GetAllVenuesAsync();
        Task<ServiceResult<IEnumerable<VenueResponse>>> GetPagedAllVenuesAsync(int pageNumber, int pageSize);
        Task<ServiceResult<VenueResponse>> GetByIdAsync(int id);

        Task<ServiceResult<VenueWithEventsResponse>> GetVenueWithEventsAsync(int id);
        Task<ServiceResult<IEnumerable<VenueWithEventsResponse>>> GetVenuesWithEventsAsync();
        Task<ServiceResult<VenueAggregateResponse>> GetVenueAggregateAsync(int id);
        Task<ServiceResult<IEnumerable<VenueAggregateResponse>>> GetVenuesAggregateAsync();
        Task<ServiceResult<VenueAdminResponse>> GetVenueForAdminAsync(int id);
        Task<ServiceResult<IEnumerable<VenueAdminResponse>>> GetAllVenuesForAdminAsync();

        Task<ServiceResult<VenueResponse>> CreateAsync(CreateVenueRequest request);
        Task<ServiceResult> UpdateAsync(UpdateVenueRequest request);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult> PassiveAsync(int id);
    }
}
