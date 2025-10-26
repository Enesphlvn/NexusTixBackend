using NexusTix.Application.Features.Cities.Dto;
using NexusTix.Application.Features.Venues.Dto;

namespace NexusTix.Application.Features.Districts.Dto
{
    public record DistrictAggregateResponse(int Id, string Name, CityAggregateResponse City, List<VenueAggregateResponse> Venues);
}
