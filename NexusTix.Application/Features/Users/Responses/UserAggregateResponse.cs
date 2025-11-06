using NexusTix.Application.Features.Tickets.Responses;

namespace NexusTix.Application.Features.Users.Responses;

public record UserAggregateResponse(int Id, string FirstName, string LastName, IEnumerable<TicketResponse> Tickets, DateTimeOffset Created, DateTimeOffset? Updated, bool IsActive);
