namespace NexusTix.Application.Features.Users.Responses;

public record UserAdminResponse
{
    public int Id { get; init; }
    public string FirstName { get; init; } = default!;
    public string LastName { get; init; } = default!;
    public string Email { get; init; } = default!;
    public string? PhoneNumber { get; init; }
    public bool IsActive { get; init; }
    public DateTimeOffset Created { get; init; }
    public DateTimeOffset? Updated { get; init; }
    public IEnumerable<string> Roles { get; set; } = [];
}