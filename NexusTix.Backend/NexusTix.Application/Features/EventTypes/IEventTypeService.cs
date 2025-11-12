using NexusTix.Application.Features.EventTypes.Create;
using NexusTix.Application.Features.EventTypes.Responses;
using NexusTix.Application.Features.EventTypes.Update;

namespace NexusTix.Application.Features.EventTypes
{
    public interface IEventTypeService
    {
        Task<ServiceResult<IEnumerable<EventTypeResponse>>> GetAllEventTypesAsync();
        Task<ServiceResult<IEnumerable<EventTypeResponse>>> GetPagedAllEventTypesAsync(int pageNumber, int pageSize);
        Task<ServiceResult<EventTypeResponse>> GetByIdAsync(int id);

        Task<ServiceResult<EventTypeWithEventsResponse>> GetEventTypeWithEventsAsync(int id);
        Task<ServiceResult<IEnumerable<EventTypeWithEventsResponse>>> GetEventTypesWithEventsAsync();
        Task<ServiceResult<EventTypeAggregateResponse>> GetEventTypeAggregateAsync(int id);
        Task<ServiceResult<IEnumerable<EventTypeAggregateResponse>>> GetEventTypesAggregateAsync();

        Task<ServiceResult<EventTypeResponse>> CreateAsync(CreateEventTypeRequest request);
        Task<ServiceResult> UpdateAsync(UpdateEventTypeRequest request);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult> PassiveAsync(int id);
    }
}
