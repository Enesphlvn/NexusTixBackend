using NexusTix.Application.Common.BaseRules;

namespace NexusTix.Application.Features.Venues.Rules
{
    public interface IVenueBusinessRules : IBaseBusinessRules
    {
        Task CheckIfDistrictExists(int districtId);
        Task CheckIfVenueExists(int venueId);
        Task CheckIfVenueNameExistsWhenCreating(string venueName);
        Task CheckIfVenueNameExistsWhenUpdating(int venueId, string venueName);
        Task CheckIfVenueHasActiveEvents(int venueId);
    }
}
