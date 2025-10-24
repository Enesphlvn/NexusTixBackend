using NexusTix.Application.Features.Venues.Dto;

namespace NexusTix.Application.Features.Cities.Dto
{
    public record CityWithVenuesResponse(int Id, string Name, IEnumerable<VenueResponse> Venues);
}
