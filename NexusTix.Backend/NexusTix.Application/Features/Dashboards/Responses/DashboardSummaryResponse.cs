namespace NexusTix.Application.Features.Dashboards.Responses;

public record DashboardSummaryResponse(decimal TotalRevenue, int TotalTicketsSold, int ActiveEventsCount, int TotalUsersCount);