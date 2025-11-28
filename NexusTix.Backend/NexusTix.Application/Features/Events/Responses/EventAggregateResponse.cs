using NexusTix.Application.Features.EventTypes.Responses;
using NexusTix.Application.Features.Venues.Responses;

namespace NexusTix.Application.Features.Events.Responses;

public record EventAggregateResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public DateTimeOffset Date { get; init; }
    public decimal Price { get; init; }
    public string? Description { get; init; }
    public int Capacity { get; init; }
    public EventTypeResponse EventType { get; init; } = default!;
    public VenueWithLocationResponse Venue { get; init; } = default!;
    public IEnumerable<string> ArtistNames { get; init; } = [];
}
