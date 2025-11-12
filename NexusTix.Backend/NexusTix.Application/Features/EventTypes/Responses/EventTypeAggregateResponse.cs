using NexusTix.Application.Features.Events.Responses;

namespace NexusTix.Application.Features.EventTypes.Responses;

public record EventTypeAggregateResponse(int Id, string Name, IEnumerable<EventResponse> Events);
