using AutoMapper;
using NexusTix.Application.Features.Events.Create;
using NexusTix.Application.Features.Events.Responses;
using NexusTix.Application.Features.Events.Update;
using NexusTix.Domain.Entities;

namespace NexusTix.Application.Features.Events
{
    public class EventProfileMapping : Profile
    {
        public EventProfileMapping()
        {
            CreateMap<CreateEventRequest, Event>();
            CreateMap<UpdateEventRequest, Event>();

            CreateMap<Event, EventResponse>();
            CreateMap<Event, EventByEventTypeResponse>();
            CreateMap<Event, EventByVenueResponse>();
            CreateMap<Event, EventByUserTicketsResponse>();
            CreateMap<Event, EventAggregateResponse>();
        }
    }
}
