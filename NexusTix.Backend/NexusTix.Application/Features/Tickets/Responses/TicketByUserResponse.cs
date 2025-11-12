using NexusTix.Application.Features.Users.Responses;

namespace NexusTix.Application.Features.Tickets.Responses;

public record TicketByUserResponse(int Id, Guid QRCodeGuid, DateTimeOffset PurchaseDate, bool IsUsed, int EventId, UserResponse User);
