using AutoMapper;
using NexusTix.Application.Features.Districts.Create;
using NexusTix.Application.Features.Districts.Dto;
using NexusTix.Application.Features.Districts.Update;
using NexusTix.Domain.Entities;

namespace NexusTix.Application.Features.Districts
{
    public class DistrictProfileMapping : Profile
    {
        public DistrictProfileMapping()
        {
            CreateMap<CreateDistrictRequest, District>();
            CreateMap<UpdateDistrictRequest, District>();

            CreateMap<District, DistrictResponse>();
            CreateMap<District, DistrictWithVenuesResponse>();
            CreateMap<District, DistrictAggregateResponse>();
        }
    }
}
