using NexusTix.Application.Features.Dashboards.Responses;
using NexusTix.Domain.Entities.Dashboard;
using NexusTix.Persistence.Repositories.Dashboards;
using System.Net;

namespace NexusTix.Application.Features.Dashboards
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;
        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<ServiceResult<DashboardChartResponse>> GetDashboardChartsAsync()
        {
            try
            {
                var revenueData = await _dashboardRepository.GetMonthlyRevenuesAsync();
                var salesData = await _dashboardRepository.GetCategorySalesAsync();

                var monthlyRevenuesDto = revenueData.Select(d => new MonthlyRevenueResponse
                (
                    $"{d.Month}/{d.Year}",
                    d.Amount
                )).ToList();

                var categorySalesDto = salesData.Select(d => new CategorySalesResponse
                (
                    d.CategoryName,
                    d.TicketCount
                )).ToList();

                var response = new DashboardChartResponse(monthlyRevenuesDto, categorySalesDto);

                return ServiceResult<DashboardChartResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return ServiceResult<DashboardChartResponse>.Fail($"Grafik verileri alınamadı: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResult<DashboardSummaryResponse>> GetDashboardSummaryAsync()
        {
            try
            {
                var revenueData = await _dashboardRepository.GetTotalRevenueAsync();
                var ticketsData = await _dashboardRepository.GetTotalTicketsSoldAsync();
                var totalEventsData = await _dashboardRepository.GetTotalEventsCountAsync();
                var activeEventsData = await _dashboardRepository.GetActiveEventsCountAsync();
                var usersData = await _dashboardRepository.GetTotalUsersCountAsync();

                var response = new DashboardSummaryResponse
                (
                    TotalRevenue: revenueData,
                    TotalTicketsSold: ticketsData,
                    TotalEventsCount: totalEventsData,
                    ActiveEventsCount: activeEventsData,
                    TotalUsersCount: usersData
                );

                return ServiceResult<DashboardSummaryResponse>.Success(response);
            }
            catch (Exception ex)
            {
                return ServiceResult<DashboardSummaryResponse>.Fail($"Dashboard verisi alınamadı: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }
    }
}
