namespace NexusTix.Application.Features.Auth.Requests;

public record ResetPasswordRequest(string Email, string Token, string NewPassword, string NewPasswordConfirm);