namespace NexusTix.Application.Features.Auth.Responses;

public record LoginResponse(string Token, DateTime Expiration, string Email, string FirstName, string LastName, IEnumerable<string> Roles);
