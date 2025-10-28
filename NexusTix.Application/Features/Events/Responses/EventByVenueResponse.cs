using NexusTix.Application.Features.Venues.Responses;

namespace NexusTix.Application.Features.Events.Responses
{
    public record EventByVenueResponse(int Id, string Name, DateTimeOffset Date, decimal Price, string? Description, int EventTypeId, VenueResponse Venue);
}
