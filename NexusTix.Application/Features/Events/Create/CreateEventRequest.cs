namespace NexusTix.Application.Features.Events.Create
{
    public record CreateEventRequest(string Name, DateTimeOffset Date, decimal Price, string? Description, int EventTypeId, int VenueId);
}
