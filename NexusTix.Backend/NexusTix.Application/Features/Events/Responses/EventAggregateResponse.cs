using NexusTix.Application.Features.EventTypes.Responses;
using NexusTix.Application.Features.Tickets.Responses;
using NexusTix.Application.Features.Venues.Responses;

namespace NexusTix.Application.Features.Events.Responses;

public record EventAggregateResponse(int Id, string Name, DateTimeOffset Date, decimal Price, string? Description, int Capacity, EventTypeResponse EventType, VenueWithLocationResponse Venue);
