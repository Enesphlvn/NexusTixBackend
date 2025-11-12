using NexusTix.Application.Features.Districts.Responses;

namespace NexusTix.Application.Features.Cities.Responses;

public record CityWithDistrictsResponse(int Id, string Name, IEnumerable<DistrictResponse> Districts);
