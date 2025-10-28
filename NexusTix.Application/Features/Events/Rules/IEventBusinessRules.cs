using NexusTix.Application.Common.BaseRules;

namespace NexusTix.Application.Features.Events.Rules
{
    public interface IEventBusinessRules : IBaseBusinessRules
    {
        Task CheckIfEventExists(int eventId);
        Task CheckIfEventTypeExists(int eventTypeId);
        Task CheckIfVenueExists(int venueId);
        Task CheckIfVenueIsAvailableOnDateCreating(int venueId, DateTimeOffset date);
        Task CheckIfVenueIsAvailableOnDateUpdating(int eventId, int venueId, DateTimeOffset date);
        Task CheckIfEventNameExistsWhenCreating(string eventName);
        Task CheckIfEventNameExistsWhenUpdating(int eventId, string eventName);
        void CheckIfDateRangeIsValid(DateTimeOffset startDate, DateTimeOffset endDate);
        Task CheckIfEventHasNoTickets(int eventId);
    }
}