namespace NexusTix.Application.Features.Auth.Requests;

public record CreateUserRequest(string FirstName, string LastName, string Email, string Password, string? PhoneNumber);
