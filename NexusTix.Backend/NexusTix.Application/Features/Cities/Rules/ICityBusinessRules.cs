using NexusTix.Application.Common.BaseRules;

namespace NexusTix.Application.Features.Cities.Rules
{
    public interface ICityBusinessRules : IBaseBusinessRules
    {
        Task CheckIfCityExists(int cityId);
        Task CheckIfCityNameExistsWhenCreating(string cityName);
        Task CheckIfCityNameExistsWhenUpdating(int cityId, string cityName);
        Task CheckIfCityHasNoDistricts(int cityId);
    }
}
