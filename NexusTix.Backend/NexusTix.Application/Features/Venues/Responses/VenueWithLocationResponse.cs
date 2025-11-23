namespace NexusTix.Application.Features.Venues.Responses;

public record VenueWithLocationResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public int Capacity { get; init; }
    public string DistrictName { get; init; } = string.Empty;
    public string CityName { get; init; } = string.Empty;
}