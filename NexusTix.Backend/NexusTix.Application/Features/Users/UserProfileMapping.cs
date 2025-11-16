using AutoMapper;
using NexusTix.Application.Features.Auth.Requests;
using NexusTix.Application.Features.Users.Responses;
using NexusTix.Application.Features.Users.Update;
using NexusTix.Domain.Entities;

namespace NexusTix.Application.Features.Users
{
    public class UserProfileMapping : Profile
    {
        public UserProfileMapping()
        {
            CreateMap<UpdateUserRequest, User>()
                .ForMember(dest => dest.Email, opt => opt.Ignore())
                .ForMember(dest => dest.UserName, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            CreateMap<User, UserResponse>();
            CreateMap<User, UserWithTicketsResponse>();
            CreateMap<User, UserAggregateResponse>();
            CreateMap<User, UserAdminResponse>()
                .ForMember(dest => dest.Roles, opt => opt.Ignore());
        }
    }
}
