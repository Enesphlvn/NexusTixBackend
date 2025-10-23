using NexusTix.Domain.Entities;

namespace NexusTix.Persistence.Repositories.Tickets
{
    public interface ITicketRepository : IGenericRepository<Ticket, int>
    {
        Task<IEnumerable<Ticket>> GetTicketsByEventAsync(int eventId);
        Task<Ticket?> GetTicketWithDetailAsync(int id);
        Task<IEnumerable<Ticket>> GetTicketsWithDetailAsync();
        Task<int> GetTicketCountByEventAsync(int eventId);
        Task<IEnumerable<Ticket>> GetTicketsByDateRangeAsync(DateTimeOffset startDate, DateTimeOffset endDate);
    }
}
