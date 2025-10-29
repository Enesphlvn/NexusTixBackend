namespace NexusTix.Application.Features.Users.Update;

public record UpdateUserEmailRequest(int Id, string NewEmail, string CurrentPassword);
