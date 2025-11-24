namespace NexusTix.Application.Features.Tickets.CheckIn;

public record CheckInTicketRequest(Guid QRCodeGuid, int EventId);
