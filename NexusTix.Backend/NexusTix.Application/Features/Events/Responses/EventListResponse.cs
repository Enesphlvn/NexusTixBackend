namespace NexusTix.Application.Features.Events.Responses;

public record EventListResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTimeOffset Date { get; init; }
    public decimal Price { get; init; }
    public string? Description { get; init; }
    public int Capacity { get; init; }
    public int EventTypeId { get; init; }
    public int VenueId { get; init; }
    public string EventTypeName { get; init; } = string.Empty;
    public string VenueName { get; init; } = string.Empty;
    public string DistrictName { get; init; } = string.Empty;
    public string CityName { get; init; } = string.Empty;
}