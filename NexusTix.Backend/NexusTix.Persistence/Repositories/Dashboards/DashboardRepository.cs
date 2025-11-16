using Microsoft.EntityFrameworkCore;
using NexusTix.Persistence.Context;

namespace NexusTix.Persistence.Repositories.Dashboards
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _context;
        public DashboardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetActiveEventsCountAsync()
        {
            return await _context.Events.CountAsync(e => e.Date > DateTimeOffset.UtcNow && e.IsActive);
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _context.Tickets.Where(x => !x.IsCancelled).SumAsync(x => x.Event.Price);
        }

        public async Task<int> GetTotalTicketsSoldAsync()
        {
            return await _context.Tickets.CountAsync(t => !t.IsCancelled);
        }

        public async Task<int> GetTotalUsersCountAsync()
        {
            return await _context.Users.CountAsync(u => u.IsActive);
        }
    }
}
