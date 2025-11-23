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
            CreateMap<Event, EventAggregateResponse>()
                .ForMember(dest => dest.Venue, opt => opt.MapFrom(src => src.Venue));
            CreateMap<Event, EventAdminResponse>()
                .ForMember(dest => dest.EventTypeName, opt => opt.MapFrom(src => src.EventType.Name))
                .ForMember(dest => dest.VenueName, opt => opt.MapFrom(src => src.Venue.Name));
            CreateMap<Event, EventListResponse>()
                .ForMember(dest => dest.VenueName, opt => opt.MapFrom(src => src.Venue.Name))
                .ForMember(dest => dest.DistrictName, opt => opt.MapFrom(src => src.Venue.District.Name))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.Venue.District.City.Name))
                .ForMember(dest => dest.EventTypeName, opt => opt.MapFrom(src => src.EventType.Name));
        }
    }
}
