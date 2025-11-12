using AutoMapper;
using NexusTix.Application.Features.Venues.Create;
using NexusTix.Application.Features.Venues.Responses;
using NexusTix.Application.Features.Venues.Update;
using NexusTix.Domain.Entities;

namespace NexusTix.Application.Features.Venues
{
    public class VenueProfileMapping : Profile
    {
        public VenueProfileMapping()
        {
            CreateMap<CreateVenueRequest, Venue>();
            CreateMap<UpdateVenueRequest, Venue>();

            CreateMap<Venue, VenueResponse>();
            CreateMap<Venue, VenueAggregateResponse>();
            CreateMap<Venue, VenueWithEventsResponse>();
        }
    }
}
