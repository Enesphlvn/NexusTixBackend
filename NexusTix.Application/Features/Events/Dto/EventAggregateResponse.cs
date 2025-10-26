using NexusTix.Application.Features.EventTypes.Dto;
using NexusTix.Application.Features.Tickets.Dto;
using NexusTix.Application.Features.Venues.Dto;

namespace NexusTix.Application.Features.Events.Dto
{
    public record EventAggregateResponse(int Id, string Name, DateTimeOffset Date, decimal Price, string? Description, EventTypeAggregateResponse EventType, VenueAggregateResponse Venue, IEnumerable<TicketAggregateResponse> Tickets);
}
