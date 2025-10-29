using AutoMapper;
using NexusTix.Application.Features.EventTypes.Create;
using NexusTix.Application.Features.EventTypes.Responses;
using NexusTix.Application.Features.EventTypes.Rules;
using NexusTix.Application.Features.EventTypes.Update;
using NexusTix.Domain.Entities;
using NexusTix.Domain.Exceptions;
using NexusTix.Persistence.Repositories;
using System.Net;

namespace NexusTix.Application.Features.EventTypes
{
    public class EventTypeService : IEventTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEventTypeBusinessRules _eventTypeRules;

        public EventTypeService(IUnitOfWork unitOfWork, IMapper mapper, IEventTypeBusinessRules eventTypeRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _eventTypeRules = eventTypeRules;
        }

        public async Task<ServiceResult<EventTypeResponse>> CreateAsync(CreateEventTypeRequest request)
        {
            try
            {
                await _eventTypeRules.CheckIfEventTypeNameExistsWhenCreating(request.Name);

                var newEventType = _mapper.Map<EventType>(request);

                await _unitOfWork.EventTypes.AddAsync(newEventType);
                await _unitOfWork.SaveChangesAsync();

                var eventTypeAsDto = _mapper.Map<EventTypeResponse>(newEventType);

                return ServiceResult<EventTypeResponse>.SuccessAsCreated(eventTypeAsDto, $"api/eventtypes/{newEventType.Id}");
            }
            catch (BusinessException ex)
            {
                return ServiceResult<EventTypeResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            try
            {
                await _eventTypeRules.CheckIfEventTypeExists(id);
                await _eventTypeRules.CheckIfEventTypeHasNoEvents(id);

                var eventType = await _unitOfWork.EventTypes.GetByIdAsync(id);

                _unitOfWork.EventTypes.Delete(eventType!);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.Success(HttpStatusCode.NoContent);
            }
            catch (BusinessException ex)
            {
                return ServiceResult.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<EventTypeResponse>>> GetAllEventTypesAsync()
        {
            var eventTypes = await _unitOfWork.EventTypes.GetAllAsync();
            var eventTypesAsDto = _mapper.Map<IEnumerable<EventTypeResponse>>(eventTypes);

            return ServiceResult<IEnumerable<EventTypeResponse>>.Success(eventTypesAsDto);
        }

        public async Task<ServiceResult<EventTypeResponse>> GetByIdAsync(int id)
        {
            try
            {
                await _eventTypeRules.CheckIfEventTypeExists(id);

                var eventType = await _unitOfWork.EventTypes.GetByIdAsync(id);

                var eventTypeAsDto = _mapper.Map<EventTypeResponse>(eventType);

                return ServiceResult<EventTypeResponse>.Success(eventTypeAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<EventTypeResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<EventTypeWithEventsResponse>>> GetEventTypesWithEventsAsync()
        {
            var eventTypes = await _unitOfWork.EventTypes.GetEventTypesWithEventsAsync();
            var eventTypesAsDto = _mapper.Map<IEnumerable<EventTypeWithEventsResponse>>(eventTypes);

            return ServiceResult<IEnumerable<EventTypeWithEventsResponse>>.Success(eventTypesAsDto);
        }

        public async Task<ServiceResult<EventTypeWithEventsResponse>> GetEventTypeWithEventsAsync(int id)
        {
            try
            {
                await _eventTypeRules.CheckIfEventTypeExists(id);

                var eventType = await _unitOfWork.EventTypes.GetEventTypeWithEventsAsync(id);
                var eventTypeAsDto = _mapper.Map<EventTypeWithEventsResponse>(eventType);

                return ServiceResult<EventTypeWithEventsResponse>.Success(eventTypeAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<EventTypeWithEventsResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<EventTypeResponse>>> GetPagedAllEventTypesAsync(int pageNumber, int pageSize)
        {
            try
            {
                _eventTypeRules.CheckIfPagingParametersAreValid(pageNumber, pageSize);

                var eventTypes = await _unitOfWork.EventTypes.GetAllPagedAsync(pageNumber, pageSize);
                var eventTypesAsDto = _mapper.Map<IEnumerable<EventTypeResponse>>(eventTypes);

                return ServiceResult<IEnumerable<EventTypeResponse>>.Success(eventTypesAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<IEnumerable<EventTypeResponse>>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult> PassiveAsync(int id)
        {
            try
            {
                await _eventTypeRules.CheckIfEventTypeExists(id);

                await _unitOfWork.EventTypes.PassiveAsync(id);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.Success(HttpStatusCode.NoContent);
            }
            catch (BusinessException ex)
            {
                return ServiceResult.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult> UpdateAsync(UpdateEventTypeRequest request)
        {
            try
            {
                await _eventTypeRules.CheckIfEventTypeExists(request.Id);
                await _eventTypeRules.CheckIfEventTypeNameExistsWhenUpdating(request.Id, request.Name);

                var eventType = await _unitOfWork.EventTypes.GetByIdAsync(request.Id);

                _mapper.Map(request, eventType);

                _unitOfWork.EventTypes.Update(eventType!);
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
