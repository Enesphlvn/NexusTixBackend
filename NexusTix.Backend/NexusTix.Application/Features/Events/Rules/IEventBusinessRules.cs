using NexusTix.Application.Common.BaseRules;

namespace NexusTix.Application.Features.Events.Rules
{
    public interface IEventBusinessRules : IBaseBusinessRules
    {
        Task CheckIfEventExists(int eventId);
        Task CheckIfEventTypeExists(int eventTypeId);
        Task CheckIfDistrictExists(int districtId);
        Task CheckIfVenueExists(int venueId);
        Task CheckIfUserExists(int userId);
        Task CheckIfCityExists(int cityId);
        Task CheckIfArtistExists(int artistId);
        Task CheckIfVenueIsAvailableOnDateCreating(int venueId, DateTimeOffset date);
        Task CheckIfVenueIsAvailableOnDateUpdating(int eventId, int venueId, DateTimeOffset date);
        Task CheckIfEventNameExistsWhenCreating(string eventName);
        Task CheckIfEventNameExistsWhenUpdating(int eventId, string eventName);
        void CheckIfDateRangeIsValid(DateTimeOffset startDate, DateTimeOffset endDate);
        void CheckIfPriceRangeIsValid(decimal minPrice, decimal maxPrice);
        void CheckIfNumberOfEventsIsValid(int numberOfEvents);
        Task CheckIfEventHasNoTickets(int eventId);
    }
}