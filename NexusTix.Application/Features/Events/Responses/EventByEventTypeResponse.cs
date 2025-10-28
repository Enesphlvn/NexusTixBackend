using NexusTix.Application.Features.EventTypes.Responses;

namespace NexusTix.Application.Features.Events.Responses
{
    public record EventByEventTypeResponse(int Id, string Name, DateTimeOffset Date, decimal Price, string? Description, EventTypeResponse EventType, int VenueId);
}
