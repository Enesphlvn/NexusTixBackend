using NexusTix.Application.Features.Tickets.Responses;

namespace NexusTix.Application.Features.Events.Responses
{
    public record EventByUserTicketsResponse(int Id, string Name, DateTimeOffset Date, decimal Price, string? Description, int EventTypeId, int VenueId, IEnumerable<TicketResponse> Tickets);
}
