namespace NexusTix.Application.Features.Events.Responses;

public record EventResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTimeOffset Date { get; init; }
    public decimal Price { get; init; }
    public string? Description { get; init; }
    public int Capacity { get; init; }
    public int EventTypeId { get; init; }
    public int VenueId { get; init; }
    public IEnumerable<int> ArtistIds { get; set; } = [];
}
