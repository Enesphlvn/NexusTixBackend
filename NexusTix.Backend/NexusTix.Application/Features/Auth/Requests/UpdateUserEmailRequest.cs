namespace NexusTix.Application.Features.Auth.Requests;

public record UpdateUserEmailRequest(int Id, string NewEmail, string CurrentPassword);
