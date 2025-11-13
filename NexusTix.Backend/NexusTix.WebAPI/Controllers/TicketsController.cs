using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusTix.Application.Features.Tickets;
using NexusTix.Application.Features.Tickets.CheckIn;
using NexusTix.Application.Features.Tickets.Create;
using System.Security.Claims;

namespace NexusTix.WebAPI.Controllers
{
    [Authorize]
    public class TicketsController : BaseController
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllTickets()
        {
            return CreateActionResult(await _ticketService.GetAllTicketsAsync());
        }

        [HttpGet("paged")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPagedAllTickets([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return CreateActionResult(await _ticketService.GetPagedAllTicketsAsync(pageNumber, pageSize));
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTicket(int id)
        {
            return CreateActionResult(await _ticketService.GetByIdAsync(id));
        }

        [HttpGet("{id:int}/aggregate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTicketAggregate(int id)
        {
            return CreateActionResult(await _ticketService.GetTicketAggregateAsync(id));
        }

        [HttpGet("aggregate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTicketsAggregate()
        {
            return CreateActionResult(await _ticketService.GetTicketsAggregateAsync());
        }

        [HttpGet("event/{eventId:int}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> GetTicketsByEvent(int eventId)
        {
            return CreateActionResult(await _ticketService.GetTicketsByEventAsync(eventId));
        }

        [HttpGet("daterange")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetTicketsByDateRange([FromQuery] DateTimeOffset startDate, [FromQuery] DateTimeOffset endDate)
        {
            return CreateActionResult(await _ticketService.GetTicketsByDateRangeAsync(startDate, endDate));
        }

        [HttpGet("has-ticket")]
        [Authorize]
        public async Task<IActionResult> HasUserTicketForEvent([FromQuery] int eventId)
        {
            var authenticatedUserId = GetAuthenticatedUserId();
            return CreateActionResult(await _ticketService.HasUserTicketForEventAsync(authenticatedUserId, eventId));
        }

        [HttpGet("event/{eventId:int}/count")]
        [AllowAnonymous]
        public async Task<IActionResult> GetTicketCountByEvent(int eventId)
        {
            return CreateActionResult(await _ticketService.GetTicketCountByEventAsync(eventId));
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMyTickets()
        {
            var authenticatedUserId = GetAuthenticatedUserId();
            var result = await _ticketService.GetTicketsByUserAsync(authenticatedUserId);
            return CreateActionResult(result);
        }

        [HttpPut("{id:int}/cancel")]
        [Authorize]
        public async Task<IActionResult> CancelTicket(int id)
        {
            var authenticatedUserId = GetAuthenticatedUserId();

            return CreateActionResult(await _ticketService.CancelTicketAsync(id, authenticatedUserId));
        }

        [HttpPost]
        [Authorize(Roles = "User, Admin, Manager")]
        public async Task<IActionResult> Create([FromBody] CreateTicketRequest request)
        {
            var authenticatedUserId = GetAuthenticatedUserId();
            return CreateActionResult(await _ticketService.CreateAsync(request, authenticatedUserId));
        }

        [HttpPut("checkin")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> CheckIn([FromBody] CheckInTicketRequest request)
        {
            return CreateActionResult(await _ticketService.CheckInAsync(request));
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResult(await _ticketService.DeleteAsync(id));
        }

        [HttpPatch("{id:int}/passive")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Passive(int id)
        {
            return CreateActionResult(await _ticketService.PassiveAsync(id));
        }

        [NonAction]
        private int GetAuthenticatedUserId()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(userIdString!);
        }
    }
}
