using NexusTix.Application.Common.Rules;

namespace NexusTix.Application.Features.Districts.Rules
{
    public interface IDistrictBusinessRules : IPagingBusinessRules
    {
        Task CheckIfDistrictExists(int districtId);
        Task CheckIfCityExists(int cityId);
        Task CheckIfDistrictNameExistsWhenCreating(string districtName);
        Task CheckIfDistrictNameExistsWhenUpdating(int districtId, string districtName);
    }
}
