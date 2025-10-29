using NexusTix.Application.Features.Venues.Responses;

namespace NexusTix.Application.Features.Districts.Responses;

public record DistrictWithVenuesResponse(int Id, string Name, int CityId, IEnumerable<VenueResponse> Venues);
