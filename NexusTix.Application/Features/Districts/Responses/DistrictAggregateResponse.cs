using NexusTix.Application.Features.Cities.Responses;
using NexusTix.Application.Features.Venues.Responses;

namespace NexusTix.Application.Features.Districts.Responses
{
    public record DistrictAggregateResponse(int Id, string Name, CityAggregateResponse City, List<VenueAggregateResponse> Venues);
}
