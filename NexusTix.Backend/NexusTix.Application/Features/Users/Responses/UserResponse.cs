namespace NexusTix.Application.Features.Users.Responses;

public record UserResponse(int Id, string FirstName, string LastName, string Email, string? PhoneNumber, bool IsActive, DateTimeOffset Created, DateTimeOffset? Updated);