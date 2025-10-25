using NexusTix.Application.Features.Cities.Dto;

namespace NexusTix.Application.Features.Districts.Dto
{
    public record DistrictsWithCityResponse(int Id, string Name, CityResponse City);
}
