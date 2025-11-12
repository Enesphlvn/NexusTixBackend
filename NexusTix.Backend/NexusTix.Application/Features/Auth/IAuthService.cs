using NexusTix.Application.Features.Auth.Requests;
using NexusTix.Application.Features.Auth.Responses;
using NexusTix.Application.Features.Users.Responses;

namespace NexusTix.Application.Features.Auth
{
    public interface IAuthService
    {
        Task<ServiceResult<UserResponse>> RegisterAsync(CreateUserRequest request);
        Task<ServiceResult<LoginResponse>> LoginAsync(LoginRequest request);
        Task<ServiceResult> UpdateEmailAsync(UpdateUserEmailRequest request, int authenticatedUserId);
        Task<ServiceResult> UpdatePasswordAsync(UpdateUserPasswordRequest request, int authenticatedUserId);
    }
}
