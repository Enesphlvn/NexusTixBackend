namespace NexusTix.Application.Features.Users.Update;

public record UpdateUserPasswordRequest(int Id, string CurrentPassword, string NewPassword, string NewPasswordConfirm);