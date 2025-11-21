using NexusTix.Domain.Entities.Dashboard;

namespace NexusTix.Persistence.Repositories.Dashboards
{
    public interface IDashboardRepository
    {
        Task<decimal> GetTotalRevenueAsync();
        Task<int> GetTotalTicketsSoldAsync();
        Task<int> GetActiveEventsCountAsync();
        Task<int> GetTotalEventsCountAsync();
        Task<int> GetTotalUsersCountAsync();
        Task<IEnumerable<MonthlyRevenueData>> GetMonthlyRevenuesAsync();
        Task<IEnumerable<CategorySalesData>> GetCategorySalesAsync();
    }
}
