using AutoMapper;
using NexusTix.Application.Features.Cities.Create;
using NexusTix.Application.Features.Cities.Dto;
using NexusTix.Application.Features.Cities.Update;
using NexusTix.Domain.Entities;

namespace NexusTix.Application.Features.Cities
{
    public class CityProfileMapping : Profile
    {
        public CityProfileMapping()
        {
            CreateMap<CreateCityRequest, City>();
            CreateMap<UpdateCityRequest, City>();
            CreateMap<City, CityResponse>();
            CreateMap<City, CityWithDistrictsResponse>();
            CreateMap<City, CityWithVenuesResponse>();
            CreateMap<City, CityWithDistrictsAndVenuesResponse>();
        }
    }
}
