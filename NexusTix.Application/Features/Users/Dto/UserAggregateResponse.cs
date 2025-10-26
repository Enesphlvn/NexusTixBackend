using NexusTix.Application.Features.Tickets.Dto;

namespace NexusTix.Application.Features.Users.Dto
{
    public record UserAggregateResponse(int Id, string FirstName, string LastName, IEnumerable<TicketAggregateResponse> Tickets, DateTimeOffset Created, DateTimeOffset? Updated, bool IsActive);
}
