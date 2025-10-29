using NexusTix.Application.Features.Tickets.Responses;

namespace NexusTix.Application.Features.Users.Responses;

public record GetUserWithTicketsResponse(int Id, string FirstName, string LastName, string Email, string? PhoneNumber, IEnumerable<TicketResponse> Tickets, bool IsActive, DateTimeOffset Created, DateTimeOffset? Updated);
