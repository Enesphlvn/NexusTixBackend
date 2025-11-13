namespace NexusTix.Application.Features.Tickets.Responses;

public record TicketByUserResponse
{
    public int Id { get; init; }
    public Guid QRCodeGuid { get; init; }
    public DateTimeOffset PurchaseDate { get; init; }
    public bool IsUsed { get; init; }
    public int EventId { get; init; }
    public string EventName { get; init; } = string.Empty;
    public DateTimeOffset EventDate { get; init; }
    public string VenueName { get; init; } = string.Empty;
    public string CityName { get; init; } = string.Empty;
}