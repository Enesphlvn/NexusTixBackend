using Microsoft.EntityFrameworkCore;
using NexusTix.Domain.Entities.Dashboard;
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

        public async Task<IEnumerable<CategorySalesData>> GetCategorySalesAsync()
        {
            var data = await _context.Tickets
                .Where(x => !x.IsCancelled)
                .GroupBy(x => x.Event.EventType.Name)
                .Select(x => new CategorySalesData
                (
                    x.Key,
                    x.Count()
                )).ToListAsync();

            return data;
        }

        public async Task<IEnumerable<MonthlyRevenueData>> GetMonthlyRevenuesAsync()
        {
            var sixMonthsAgo = DateTimeOffset.UtcNow.AddMonths(-6);

            var data = await _context.Tickets
                .Where(x => x.PurchaseDate >= sixMonthsAgo && !x.IsCancelled)
                .GroupBy(x => new { x.PurchaseDate.Year, x.PurchaseDate.Month })
                .Select(x => new MonthlyRevenueData
                (
                    x.Key.Year,
                    x.Key.Month,
                    x.Sum(x => x.Event.Price)
                )).ToListAsync();

            return data;
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
