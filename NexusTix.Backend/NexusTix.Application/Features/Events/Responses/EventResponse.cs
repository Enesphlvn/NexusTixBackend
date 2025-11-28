namespace NexusTix.Application.Features.Events.Responses;

public record EventResponse(int Id, string Name, DateTimeOffset Date, decimal Price, string? Description, int Capacity, int EventTypeId, int VenueId, IEnumerable<int> ArtistIds);
