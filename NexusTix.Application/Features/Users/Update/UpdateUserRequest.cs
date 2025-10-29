namespace NexusTix.Application.Features.Users.Update;

public record UpdateUserRequest(int Id, string FirstName, string LastName, string? PhoneNumber);
