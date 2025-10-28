using NexusTix.Application.Features.Districts.Responses;

namespace NexusTix.Application.Features.Cities.Responses
{
    public record CityAggregateResponse(int Id, string Name, IEnumerable<DistrictWithVenuesResponse> Districts);
}
