using NexusTix.Application.Features.Venues.Responses;

namespace NexusTix.Application.Features.Cities.Responses;

public record CityWithVenuesResponse(int Id, string Name, IEnumerable<VenueResponse> Venues);
