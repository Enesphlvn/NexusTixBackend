using NexusTix.Application.Features.Events.Dto;

namespace NexusTix.Application.Features.EventTypes.Dto
{
    public record EventTypeAggregateResponse(int Id, string Name, IEnumerable<EventAggregateResponse> Events);
}
