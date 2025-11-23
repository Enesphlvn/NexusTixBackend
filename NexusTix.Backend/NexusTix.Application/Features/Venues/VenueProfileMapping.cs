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
            CreateMap<Venue, VenueAdminResponse>()
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.District.CityId));
            CreateMap<Venue, VenueWithLocationResponse>()
                .ForMember(dest => dest.DistrictName, opt => opt.MapFrom(src => src.District.Name))
                .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.District.City.Name));
        }
    }
}
