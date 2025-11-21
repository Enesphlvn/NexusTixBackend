namespace NexusTix.Application.Features.Dashboards.Responses;

public record DashboardChartResponse(IEnumerable<MonthlyRevenueResponse> MonthlyRevenues, IEnumerable<CategorySalesResponse> CategorySales);
