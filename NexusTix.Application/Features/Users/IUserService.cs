using NexusTix.Application.Features.Users.Create;
using NexusTix.Application.Features.Users.Responses;
using NexusTix.Application.Features.Users.Update;

namespace NexusTix.Application.Features.Users
{
    public interface IUserService
    {
        Task<ServiceResult<IEnumerable<UserResponse>>> GetAllUsersAsync();
        Task<ServiceResult<IEnumerable<UserResponse>>> GetPagedAllUsersAsync(int pageNumber, int pageSize);
        Task<ServiceResult<UserResponse>> GetByIdAsync(int id);

        Task<ServiceResult<UserWithTicketsResponse>> GetUserWithTicketsAsync(int id);
        Task<ServiceResult<IEnumerable<UserWithTicketsResponse>>> GetUsersWithTicketsAsync();
        Task<ServiceResult<UserAggregateResponse>> GetUserAggregateAsync(int id);
        Task<ServiceResult<IEnumerable<UserAggregateResponse>>> GetUsersAggregateAsync();

        Task<ServiceResult<UserResponse>> CreateAsync(CreateUserRequest request);
        Task<ServiceResult> UpdateAsync(UpdateUserRequest request);
        Task<ServiceResult> UpdateEmailAsync(UpdateUserEmailRequest request);
        Task<ServiceResult> UpdatePasswordAsync(UpdateUserPasswordRequest request);
        Task<ServiceResult> UpdateRoleAsync(UpdateUserRoleRequest request);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult> PassiveAsync(int id);
    }
}
