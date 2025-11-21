using NexusTix.Application.Features.Dashboards.Responses;

namespace NexusTix.Application.Features.Dashboards
{
    public interface IDashboardService
    {
        Task<ServiceResult<DashboardSummaryResponse>> GetDashboardSummaryAsync();
        Task<ServiceResult<DashboardChartResponse>> GetDashboardChartsAsync();
    }
}
