using NexusTix.Application.Features.Events.Responses;
using NexusTix.Application.Features.Users.Responses;

namespace NexusTix.Application.Features.Tickets.Responses;

public record TicketByDateRangeResponse(int Id, Guid QRCodeGuid, DateTimeOffset PurchaseDate, bool IsUsed, EventResponse Event, UserResponse User, bool IsCancelled);
