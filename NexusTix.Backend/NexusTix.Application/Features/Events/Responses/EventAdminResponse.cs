namespace NexusTix.Application.Features.Events.Responses;

public record EventAdminResponse(int Id, string Name, DateTimeOffset Date, decimal Price, string? Description, int Capacity, int EventTypeId, string EventTypeName, int VenueId, string VenueName);
