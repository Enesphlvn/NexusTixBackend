namespace NexusTix.Application.Features.Auth.Rules
{
    public interface IAuthBusinessRules
    {
        Task CheckIfUserExists(int userId);
        Task CheckIfEmailExistsWhenCreating(string email);
        Task CheckIfEmailExistsWhenUpdating(int userId, string email);
        Task CheckIfPhoneNumberExistsWhenCreating(string phoneNumber);
        Task CheckIfCurrentPasswordIsValid(int userId, string currentPassword);
        void CheckIfNewPasswordIsDifferent(string currentPassword, string newPassword);
    }
}
