namespace NexusTix.Application.Features.Events.Responses;

public record EventAdminResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTimeOffset Date { get; init; }
    public decimal Price { get; init; }
    public string? Description { get; init; }
    public int Capacity { get; init; }
    public int EventTypeId { get; init; }
    public string EventTypeName { get; init; } = string.Empty;
    public int VenueId { get; init; }
    public string VenueName { get; init; } = string.Empty;
    public bool IsActive { get; init; }
    public DateTimeOffset Created { get; init; }
    public DateTimeOffset? Updated { get; init; }
    public IEnumerable<string> ArtistNames { get; set; } = [];
}
