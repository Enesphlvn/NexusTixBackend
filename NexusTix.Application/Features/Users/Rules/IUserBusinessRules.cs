using NexusTix.Application.Common.BaseRules;

namespace NexusTix.Application.Features.Users.Rules
{
    public interface IUserBusinessRules : IBaseBusinessRules
    {
        Task CheckIfUserExists(int userId);
        Task CheckIfRoleExists(string roleName);
        Task CheckIfEmailExistsWhenCreating(string email);
        Task CheckIfEmailExistsWhenUpdating(int userId, string email);
        Task CheckIfPhoneNumberExistsWhenCreating(string phoneNumber);
        Task CheckIfPhoneNumberExistsWhenUpdating(int userId, string phoneNumber);
        Task CheckIfCurrentPasswordIsValid(int userId, string currentPassword);
        Task CheckIfUserHasNoTickets(int userId);
        void CheckIfNewPasswordIsDifferent(string currentPassword, string newPassword);
    }
}
