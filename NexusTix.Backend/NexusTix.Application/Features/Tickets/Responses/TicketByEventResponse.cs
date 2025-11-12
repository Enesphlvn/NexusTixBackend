using NexusTix.Application.Features.Events.Responses;

namespace NexusTix.Application.Features.Tickets.Responses;

public record TicketByEventResponse(int Id, Guid QRCodeGuid, DateTimeOffset PurchaseDate, bool IsUsed, EventResponse Event, int UserId);
