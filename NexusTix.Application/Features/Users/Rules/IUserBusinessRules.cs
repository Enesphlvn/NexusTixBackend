using NexusTix.Application.Common.BaseRules;

namespace NexusTix.Application.Features.Users.Rules
{
    public interface IUserBusinessRules : IBaseBusinessRules
    {
        Task CheckIfUserExists(int userId);
        Task CheckIfEmailExistsWhenCreating(string email);
        Task CheckIfEmailExistsWhenUpdating(int userId, string email);
        Task CheckIfPhoneNumberExists(string phoneNumber);
        Task CheckIfCurrentPasswordIsValid(int userId, string currentPassword);
        void CheckIfNewPasswordIsDifferent(string currentPassword, string newPassword);
    }
}
