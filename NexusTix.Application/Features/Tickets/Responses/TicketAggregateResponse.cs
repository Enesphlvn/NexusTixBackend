using NexusTix.Application.Features.Events.Responses;
using NexusTix.Application.Features.Users.Responses;

namespace NexusTix.Application.Features.Tickets.Responses
{
    public record TicketAggregateResponse(int Id, EventAggregateResponse Event, UserAggregateResponse User, Guid QRCodeGuid, DateTimeOffset PurchaseDate, bool IsUsed);
}
