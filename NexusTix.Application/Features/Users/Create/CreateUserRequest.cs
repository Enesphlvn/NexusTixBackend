namespace NexusTix.Application.Features.Users.Create
{
    public record CreateUserRequest(string FirstName, string LastName, string Email, string Password, string? PhoneNumber);
}
