using NexusTix.Application.Features.Districts.Responses;
using NexusTix.Application.Features.Events.Responses;

namespace NexusTix.Application.Features.Venues.Responses
{
    public record VenueAggregateResponse(int Id, string Name, int Capacity, DistrictAggregateResponse District, IEnumerable<EventAggregateResponse> Events);
}
