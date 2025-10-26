using NexusTix.Application.Features.Events.Dto;
using NexusTix.Application.Features.Users.Dto;

namespace NexusTix.Application.Features.Tickets.Dto
{
    public record TicketAggregateResponse(int Id, EventAggregateResponse Event, UserAggregateResponse User, Guid QRCodeGuid, DateTimeOffset PurchaseDate, bool IsUsed);
}
