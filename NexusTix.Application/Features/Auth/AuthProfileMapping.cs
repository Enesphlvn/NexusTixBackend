using AutoMapper;
using NexusTix.Application.Features.Auth.Requests;
using NexusTix.Domain.Entities;

namespace NexusTix.Application.Features.Auth
{
    public class AuthProfileMapping : Profile
    {
        public AuthProfileMapping()
        {
            CreateMap<CreateUserRequest, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.ToLower()));
        }
    }
}
