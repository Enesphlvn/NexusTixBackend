using AutoMapper;
using NexusTix.Application.Features.Events.Create;
using NexusTix.Application.Features.Events.Responses;
using NexusTix.Application.Features.Events.Rules;
using NexusTix.Application.Features.Events.Update;
using NexusTix.Domain.Entities;
using NexusTix.Domain.Exceptions;
using NexusTix.Persistence.Repositories;
using System.Net;

namespace NexusTix.Application.Features.Events
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEventBusinessRules _eventRules;

        public EventService(IUnitOfWork unitOfWork, IMapper mapper, IEventBusinessRules eventRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _eventRules = eventRules;
        }

        public async Task<ServiceResult<EventResponse>> CreateAsync(CreateEventRequest request)
        {
            try
            {
                await _eventRules.CheckIfEventNameExistsWhenCreating(request.Name);
                await _eventRules.CheckIfEventTypeExists(request.EventTypeId);
                await _eventRules.CheckIfVenueExists(request.VenueId);
                await _eventRules.CheckIfVenueIsAvailableOnDateCreating(request.VenueId, request.Date);

                var newEvent = _mapper.Map<Event>(request);

                await _unitOfWork.Events.AddAsync(newEvent);
                await _unitOfWork.SaveChangesAsync();

                var eventAsDto = _mapper.Map<EventResponse>(newEvent);

                return ServiceResult<EventResponse>.SuccessAsCreated(eventAsDto, $"api/events/{newEvent.Id}");
            }
            catch (BusinessException ex)
            {
                return ServiceResult<EventResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            try
            {
                await _eventRules.CheckIfEventExists(id);
                await _eventRules.CheckIfEventHasNoTickets(id);

                var newEvent = await _unitOfWork.Events.GetByIdAsync(id);

                _unitOfWork.Events.Delete(newEvent!);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.Success(HttpStatusCode.NoContent);
            }
            catch (BusinessException ex)
            {
                return ServiceResult.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<EventResponse>>> GetAllEventsAsync()
        {
            try
            {
                var events = await _unitOfWork.Events.GetAllAsync();

                var eventsAsDto = _mapper.Map<IEnumerable<EventResponse>>(events);

                return ServiceResult<IEnumerable<EventResponse>>.Success(eventsAsDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<EventResponse>>.Fail($"Bir hata oluştu: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResult<EventResponse>> GetByIdAsync(int id)
        {
            try
            {
                await _eventRules.CheckIfEventExists(id);

                var newEvent = await _unitOfWork.Events.GetByIdAsync(id);

                var eventAsDto = _mapper.Map<EventResponse>(newEvent);

                return ServiceResult<EventResponse>.Success(eventAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<EventResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<EventAggregateResponse>> GetEventAggregateAsync(int id)
        {
            try
            {
                await _eventRules.CheckIfEventExists(id);

                var newEvent = await _unitOfWork.Events.GetEventAggregateAsync(id);

                var eventAsDto = _mapper.Map<EventAggregateResponse>(newEvent);

                return ServiceResult<EventAggregateResponse>.Success(eventAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<EventAggregateResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<EventAggregateResponse>>> GetEventsAggregateAsync()
        {
            try
            {
                var events = await _unitOfWork.Events.GetEventsAggregateAsync();

                var eventsAsDto = _mapper.Map<IEnumerable<EventAggregateResponse>>(events);

                return ServiceResult<IEnumerable<EventAggregateResponse>>.Success(eventsAsDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<EventAggregateResponse>>.Fail($"Bir hata oluştu: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResult<IEnumerable<EventResponse>>> GetEventsByDateRangeAsync(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            try
            {
                _eventRules.CheckIfDateRangeIsValid(startDate, endDate);

                var events = await _unitOfWork.Events.GetEventsByDateRangeAsync(startDate, endDate);
                var eventsAsDto = _mapper.Map<IEnumerable<EventResponse>>(events);

                return ServiceResult<IEnumerable<EventResponse>>.Success(eventsAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<IEnumerable<EventResponse>>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<EventByEventTypeResponse>>> GetEventsByEventTypeAsync(int eventTypeId)
        {
            try
            {
                await _eventRules.CheckIfEventTypeExists(eventTypeId);

                var events = await _unitOfWork.Events.GetEventsByEventTypeAsync(eventTypeId);
                var eventsAsDto = _mapper.Map<IEnumerable<EventByEventTypeResponse>>(events);

                return ServiceResult<IEnumerable<EventByEventTypeResponse>>.Success(eventsAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<IEnumerable<EventByEventTypeResponse>>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<EventResponse>>> GetEventsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            try
            {
                _eventRules.CheckIfPriceRangeIsValid(minPrice, maxPrice);

                var events = await _unitOfWork.Events.GetEventsByPriceRangeAsync(minPrice, maxPrice);
                var eventsAsDto = _mapper.Map<IEnumerable<EventResponse>>(events);

                return ServiceResult<IEnumerable<EventResponse>>.Success(eventsAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<IEnumerable<EventResponse>>.Fail(ex.Message, ex.StatusCode);
            }


        }

        public async Task<ServiceResult<IEnumerable<EventByUserTicketsResponse>>> GetEventsByUserTicketsAsync(int userId)
        {
            try
            {
                await _eventRules.CheckIfUserExists(userId);

                var events = await _unitOfWork.Events.GetEventsByUserTicketsAsync(userId);
                var eventsAsDto = _mapper.Map<IEnumerable<EventByUserTicketsResponse>>(events);
                if (!eventsAsDto.Any())
                {
                    return ServiceResult<IEnumerable<EventByUserTicketsResponse>>.Success([]);
                }

                return ServiceResult<IEnumerable<EventByUserTicketsResponse>>.Success(eventsAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<IEnumerable<EventByUserTicketsResponse>>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<EventByVenueResponse>>> GetEventsByVenueAsync(int venueId)
        {
            try
            {
                await _eventRules.CheckIfVenueExists(venueId);

                var events = await _unitOfWork.Events.GetEventsByVenueAsync(venueId);
                var eventsAsDto = _mapper.Map<IEnumerable<EventByVenueResponse>>(events);

                return ServiceResult<IEnumerable<EventByVenueResponse>>.Success(eventsAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<IEnumerable<EventByVenueResponse>>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<EventAdminResponse>>> GetEventsForAdminAsync()
        {
            try
            {
                var events = await _unitOfWork.Events.GetFilteredEventsAsync(null, null, null, null);

                var eventsAsDto = _mapper.Map<IEnumerable<EventAdminResponse>>(events);

                return ServiceResult<IEnumerable<EventAdminResponse>>.Success(eventsAsDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<EventAdminResponse>>.Fail($"Bir hata oluştu: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResult<IEnumerable<EventByUserTicketsResponse>>> GetEventsWithHighestSalesAsync(int numberOfEvents)
        {
            try
            {
                _eventRules.CheckIfNumberOfEventsIsValid(numberOfEvents);

                var events = await _unitOfWork.Events.GetEventsWithHighestSalesAsync(numberOfEvents);
                var eventsAsDto = _mapper.Map<IEnumerable<EventByUserTicketsResponse>>(events);

                return ServiceResult<IEnumerable<EventByUserTicketsResponse>>.Success(eventsAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<IEnumerable<EventByUserTicketsResponse>>.Fail(ex.Message, ex.StatusCode);
            }


        }

        public async Task<ServiceResult<IEnumerable<EventResponse>>> GetFilteredEventsAsync(int? cityId, int? districtId, int? eventTypeId, DateTimeOffset? date)
        {
            try
            {
                if (cityId.HasValue && cityId > 0)
                {
                    await _eventRules.CheckIfCityExists(cityId!.Value);
                }

                if (districtId.HasValue && districtId > 0)
                {
                    await _eventRules.CheckIfDistrictExists(districtId!.Value);
                }

                if (eventTypeId.HasValue && eventTypeId > 0)
                {
                    await _eventRules.CheckIfEventTypeExists(eventTypeId!.Value);
                }

                var events = await _unitOfWork.Events.GetFilteredEventsAsync(cityId, districtId, eventTypeId, date);

                var eventsAsDto = _mapper.Map<IEnumerable<EventResponse>>(events);

                return ServiceResult<IEnumerable<EventResponse>>.Success(eventsAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<IEnumerable<EventResponse>>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<EventResponse>>> GetPagedAllEventsAsync(int pageNumber, int pageSize)
        {
            try
            {
                _eventRules.CheckIfPagingParametersAreValid(pageNumber, pageSize);

                var events = await _unitOfWork.Events.GetAllPagedAsync(pageNumber, pageSize);
                var eventsAsDto = _mapper.Map<IEnumerable<EventResponse>>(events);

                return ServiceResult<IEnumerable<EventResponse>>.Success(eventsAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<IEnumerable<EventResponse>>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult> PassiveAsync(int id)
        {
            try
            {
                await _eventRules.CheckIfEventExists(id);
                await _eventRules.CheckIfEventHasNoTickets(id);

                await _unitOfWork.Events.PassiveAsync(id);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.Success(HttpStatusCode.NoContent);
            }
            catch (BusinessException ex)
            {
                return ServiceResult.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult> UpdateAsync(UpdateEventRequest request)
        {
            try
            {
                await _eventRules.CheckIfEventExists(request.Id);
                await _eventRules.CheckIfEventNameExistsWhenUpdating(request.Id, request.Name);
                await _eventRules.CheckIfEventTypeExists(request.EventTypeId);
                await _eventRules.CheckIfVenueExists(request.VenueId);
                await _eventRules.CheckIfVenueIsAvailableOnDateUpdating(request.Id, request.VenueId, request.Date);

                var newEvent = await _unitOfWork.Events.GetByIdAsync(request.Id);

                _mapper.Map(request, newEvent);

                _unitOfWork.Events.Update(newEvent!);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.Success(HttpStatusCode.NoContent);
            }
            catch (BusinessException ex)
            {
                return ServiceResult.Fail(ex.Message, ex.StatusCode);
            }
        }
    }
}