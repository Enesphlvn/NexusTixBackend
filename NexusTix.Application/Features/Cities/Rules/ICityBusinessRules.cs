namespace NexusTix.Application.Features.Cities.Rules
{
    public interface ICityBusinessRules
    {
        Task CheckIfCityExists(int cityId);
        Task CheckIfCityNameExistsWhenCreating(string cityName);
        Task CheckIfCityNameExistsWhenUpdating(int cityId, string cityName);
    }
}
