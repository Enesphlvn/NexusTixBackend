namespace NexusTix.Application.Features.Events.Update;

public record UpdateEventRequest(int Id, string Name, DateTimeOffset Date, decimal Price, string? Description, int EventTypeId, int VenueId);
