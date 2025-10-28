using NexusTix.Application.Features.Events.Responses;

namespace NexusTix.Application.Features.Venues.Responses
{
    public record VenueWithEventsResponse(int Id, string Name, int Capacity, int DistrictId, IEnumerable<EventResponse> Events);
}
