using NexusTix.Application.Common.BaseRules;

namespace NexusTix.Application.Features.Users.Rules
{
    public interface IUserBusinessRules : IBaseBusinessRules
    {
        Task CheckIfUserExists(int userId);
        Task CheckIfRoleExists(string roleName);
        Task CheckIfPhoneNumberExistsWhenUpdating(int userId, string phoneNumber);
        Task CheckIfUserHasNoTickets(int userId);
    }
}
