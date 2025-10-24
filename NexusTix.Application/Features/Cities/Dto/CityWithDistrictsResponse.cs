using NexusTix.Application.Features.Districts.Dto;

namespace NexusTix.Application.Features.Cities.Dto
{
    public record CityWithDistrictsResponse(int Id, string Name, IEnumerable<DistrictResponse> Districts);
}
