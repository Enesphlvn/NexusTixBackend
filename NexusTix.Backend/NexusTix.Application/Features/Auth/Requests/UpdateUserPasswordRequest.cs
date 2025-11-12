namespace NexusTix.Application.Features.Auth.Requests;

public record UpdateUserPasswordRequest(int Id, string CurrentPassword, string NewPassword, string NewPasswordConfirm);