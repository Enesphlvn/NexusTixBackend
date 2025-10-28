using AutoMapper;
using NexusTix.Application.Features.Events.Create;
using NexusTix.Application.Features.Events.Responses;
using NexusTix.Application.Features.Events.Update;
using NexusTix.Persistence.Repositories;

namespace NexusTix.Application.Features.Events
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EventService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<ServiceResult<EventResponse>> CreateAsync(CreateEventRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<IEnumerable<EventResponse>>> GetAllEventsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<EventResponse>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<EventAggregateResponse>> GetEventAggregateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<IEnumerable<EventAggregateResponse>>> GetEventsAggregateAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<IEnumerable<EventResponse>>> GetEventsByDateRangeAsync(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<IEnumerable<EventByEventTypeResponse>>> GetEventsByEventTypeAsync(int eventTypeId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<IEnumerable<EventResponse>>> GetEventsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<IEnumerable<EventByUserTicketsResponse>>> GetEventsByUserTicketsAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<IEnumerable<EventByVenueResponse>>> GetEventsByVenueAsync(int venueId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<IEnumerable<EventByUserTicketsResponse>>> GetEventsWithHighestSalesAsync(int numberOfEvents)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<IEnumerable<EventResponse>>> GetPagedAllEventsAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> PassiveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> UpdateAsync(UpdateEventRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
