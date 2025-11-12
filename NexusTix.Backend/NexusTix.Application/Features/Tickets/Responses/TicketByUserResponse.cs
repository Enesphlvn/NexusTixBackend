namespace NexusTix.Application.Features.Tickets.Responses;

public record TicketByUserResponse(int Id, Guid QRCodeGuid, DateTimeOffset PurchaseDate, bool IsUsed, int EventId, string EventName, DateTimeOffset EventDate, string VenueName, string CityName);
