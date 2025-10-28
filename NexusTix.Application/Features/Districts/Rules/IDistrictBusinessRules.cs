using NexusTix.Application.Common.BaseRules;

namespace NexusTix.Application.Features.Districts.Rules
{
    public interface IDistrictBusinessRules : IBaseBusinessRules
    {
        Task CheckIfDistrictExists(int districtId);
        Task CheckIfCityExists(int cityId);
        Task CheckIfDistrictNameExistsWhenCreating(string districtName);
        Task CheckIfDistrictNameExistsWhenUpdating(int districtId, string districtName);
    }
}
