using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusTix.Application.Features.Dashboards;

namespace NexusTix.WebAPI.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class DashboardsController : BaseController
    {
        private readonly IDashboardService _dashboardService;
        public DashboardsController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            return CreateActionResult(await _dashboardService.GetDashboardSummaryAsync());
        }
    }
}
