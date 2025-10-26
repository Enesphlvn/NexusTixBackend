using NexusTix.Application.Features.Events.Dto;

namespace NexusTix.Application.Features.Venues.Dto
{
    public record VenueWithEventsResponse(int Id, string Name, int Capacity, int DistrictId, IEnumerable<EventResponse> Events);
}
