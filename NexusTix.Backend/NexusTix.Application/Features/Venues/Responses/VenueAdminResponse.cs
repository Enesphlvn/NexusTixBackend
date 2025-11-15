namespace NexusTix.Application.Features.Venues.Responses;

public record VenueAdminResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int Capacity { get; init; }
    public int DistrictId { get; init; }
    public int CityId { get; init; }
}
