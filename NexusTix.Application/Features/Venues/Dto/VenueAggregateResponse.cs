using NexusTix.Application.Features.Districts.Dto;
using NexusTix.Application.Features.Events.Dto;

namespace NexusTix.Application.Features.Venues.Dto
{
    public record VenueAggregateResponse(int Id, string Name, int Capacity, DistrictAggregateResponse District, IEnumerable<EventAggregateResponse> Events);
}
