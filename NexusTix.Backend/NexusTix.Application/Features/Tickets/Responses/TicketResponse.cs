namespace NexusTix.Application.Features.Tickets.Responses;

public record TicketResponse(int Id, int EventId, int UserId, Guid QRCodeGuid, DateTimeOffset PurchaseDate, bool IsUsed);
