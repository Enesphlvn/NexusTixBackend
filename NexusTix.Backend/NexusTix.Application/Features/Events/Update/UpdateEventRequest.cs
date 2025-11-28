namespace NexusTix.Application.Features.Events.Update;

public record UpdateEventRequest(int Id, string Name, DateTimeOffset Date, decimal Price, string? Description, int Capacity, int EventTypeId, int VenueId, IEnumerable<int> ArtistIds);
