using NexusTix.Application.Features.Districts.Dto;

namespace NexusTix.Application.Features.Cities.Dto
{
    public record CityAggregateResponse(int Id, string Name, IEnumerable<DistrictWithVenuesResponse> Districts);
}
