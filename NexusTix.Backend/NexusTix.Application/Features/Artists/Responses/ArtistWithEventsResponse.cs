using NexusTix.Application.Features.Events.Responses;

namespace NexusTix.Application.Features.Artists.Responses;

public record ArtistWithEventsResponse(int Id, string Name, string? Bio, string? ImageUrl, IEnumerable<EventListResponse> Events);