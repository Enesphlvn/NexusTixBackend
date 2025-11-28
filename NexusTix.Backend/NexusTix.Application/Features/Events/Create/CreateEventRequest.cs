namespace NexusTix.Application.Features.Events.Create;

public record CreateEventRequest(string Name, DateTimeOffset Date, decimal Price, string? Description, int Capacity, int EventTypeId, int VenueId, IEnumerable<int> ArtistIds);
