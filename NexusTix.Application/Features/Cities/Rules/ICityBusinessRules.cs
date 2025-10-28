using NexusTix.Application.Common.Rules;

namespace NexusTix.Application.Features.Cities.Rules
{
    public interface ICityBusinessRules : IPagingBusinessRules
    {
        Task CheckIfCityExists(int cityId);
        Task CheckIfCityNameExistsWhenCreating(string cityName);
        Task CheckIfCityNameExistsWhenUpdating(int cityId, string cityName);
    }
}
