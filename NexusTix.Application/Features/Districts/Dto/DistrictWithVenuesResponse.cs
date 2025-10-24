using NexusTix.Application.Features.Venues.Dto;

namespace NexusTix.Application.Features.Districts.Dto
{
    public record DistrictWithVenuesResponse(int Id, string Name, int CityId, IEnumerable<VenueResponse> Venues);
}
