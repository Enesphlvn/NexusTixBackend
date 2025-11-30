using NexusTix.Application.Features.Artists.Responses;
using NexusTix.Application.Features.Events.Create;
using NexusTix.Application.Features.Events.Responses;
using NexusTix.Application.Features.Events.Update;

namespace NexusTix.Application.Features.Events
{
    public interface IEventService
    {
        Task<ServiceResult<IEnumerable<EventResponse>>> GetAllEventsAsync();
        Task<ServiceResult<IEnumerable<EventResponse>>> GetPagedAllEventsAsync(int pageNumber, int pageSize);
        Task<ServiceResult<EventResponse>> GetByIdAsync(int id);

        Task<ServiceResult<EventAggregateResponse>> GetEventAggregateAsync(int id);
        Task<ServiceResult<IEnumerable<EventAggregateResponse>>> GetEventsAggregateAsync();
        Task<ServiceResult<IEnumerable<EventByEventTypeResponse>>> GetEventsByEventTypeAsync(int eventTypeId);
        Task<ServiceResult<IEnumerable<EventByVenueResponse>>> GetEventsByVenueAsync(int venueId);
        Task<ServiceResult<IEnumerable<EventByUserTicketsResponse>>> GetEventsByUserTicketsAsync(int userId);
        Task<ServiceResult<IEnumerable<EventResponse>>> GetEventsByDateRangeAsync(DateTimeOffset startDate, DateTimeOffset endDate);
        Task<ServiceResult<IEnumerable<EventResponse>>> GetEventsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<ServiceResult<IEnumerable<EventByUserTicketsResponse>>> GetEventsWithHighestSalesAsync(int numberOfEvents);
        Task<ServiceResult<IEnumerable<EventListResponse>>> GetFilteredEventsAsync(int? cityId, int? districtId, int? eventTypeId, int? artistId, DateTimeOffset? date);
        Task<ServiceResult<IEnumerable<EventAdminResponse>>> GetAllEventsForAdminAsync();
        Task<ServiceResult<IEnumerable<EventListResponse>>> GetAllEventsForCheckInAsync();
        Task<ServiceResult<EventAdminResponse>> GetEventForAdminAsync(int id);

        Task<ServiceResult<EventResponse>> CreateAsync(CreateEventRequest request);
        Task<ServiceResult> UpdateAsync(UpdateEventRequest request);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult> PassiveAsync(int id);
    }
}
