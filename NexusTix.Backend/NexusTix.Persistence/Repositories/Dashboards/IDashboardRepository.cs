namespace NexusTix.Persistence.Repositories.Dashboards
{
    public interface IDashboardRepository
    {
        Task<decimal> GetTotalRevenueAsync();
        Task<int> GetTotalTicketsSoldAsync();
        Task<int> GetActiveEventsCountAsync();
        Task<int> GetTotalUsersCountAsync();
    }
}
