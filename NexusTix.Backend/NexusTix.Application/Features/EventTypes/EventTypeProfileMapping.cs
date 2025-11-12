using AutoMapper;
using NexusTix.Application.Features.EventTypes.Create;
using NexusTix.Application.Features.EventTypes.Responses;
using NexusTix.Application.Features.EventTypes.Update;
using NexusTix.Domain.Entities;

namespace NexusTix.Application.Features.EventTypes
{
    public class EventTypeProfileMapping : Profile
    {
        public EventTypeProfileMapping()
        {
            CreateMap<CreateEventTypeRequest, EventType>();
            CreateMap<UpdateEventTypeRequest, EventType>();

            CreateMap<EventType, EventTypeResponse>();
            CreateMap<EventType, EventTypeAggregateResponse>();
            CreateMap<EventType, EventTypeWithEventsResponse>();
        }
    }
}
