namespace NexusTix.Application.Features.Dashboards.Responses;

public record DashboardSummaryResponse(decimal TotalRevenue, int TotalTicketsSold, int TotalEventsCount, int ActiveEventsCount, int TotalUsersCount);