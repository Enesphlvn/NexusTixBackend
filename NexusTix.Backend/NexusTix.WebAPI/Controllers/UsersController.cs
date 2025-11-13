using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusTix.Application;
using NexusTix.Application.Features.Tickets;
using NexusTix.Application.Features.Users;
using NexusTix.Application.Features.Users.Update;
using System.Net;
using System.Security.Claims;

namespace NexusTix.WebAPI.Controllers
{
    [Authorize]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ITicketService _ticketService;

        public UsersController(IUserService userService, ITicketService ticketService)
        {
            _userService = userService;
            _ticketService = ticketService;
        }

        // --- YÖNETİM (Admin/Manager) ENDPOINT'LERİ ---
        [HttpGet]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> GetAllUsers()
        {
            return CreateActionResult(await _userService.GetAllUsersAsync());
        }

        [HttpGet("paged")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> GetPagedAllUsers([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return CreateActionResult(await _userService.GetPagedAllUsersAsync(pageNumber, pageSize));
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUser(int id)
        {
            return CreateActionResult(await _userService.GetByIdAsync(id));
        }

        [HttpGet("{id:int}/aggregate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserAggregate(int id)
        {
            return CreateActionResult(await _userService.GetUserAggregateAsync(id));
        }

        [HttpGet("aggregate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsersAggregate()
        {
            return CreateActionResult(await _userService.GetUsersAggregateAsync());
        }

        // --- KULLANICI (Kendi) ENDPOINT'LERİ ---
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMyProfile()
        {
            var authenticatedUserId = GetAuthenticatedUserId();
            return CreateActionResult(await _userService.GetByIdAsync(authenticatedUserId));
        }

        [HttpGet("me/tickets")]
        [Authorize]
        public async Task<IActionResult> GetMyTickets()
        {
            var authenticatedUserId = GetAuthenticatedUserId();
            return CreateActionResult(await _ticketService.GetTicketsByUserAsync(authenticatedUserId));
        }

        [HttpPut("me")]
        [Authorize]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateUserRequest request)
        {
            var authenticatedUserId = GetAuthenticatedUserId();
            if (authenticatedUserId != request.Id)
            {
                return CreateActionResult(ServiceResult.Fail("Kendi ID'niz dışındaki bir profili güncelleyemezsiniz.", HttpStatusCode.BadRequest));
            }

            return CreateActionResult(await _userService.UpdateAsync(request));
        }

        // --- YÖNETİM KOMUTLARI ---
        [HttpPut("{id:int}/role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] UpdateUserRoleRequest request)
        {
            if (id != request.Id)
            {
                return CreateActionResult(ServiceResult.Fail("Route ID ve Request Body ID eşleşmiyor.", HttpStatusCode.BadRequest));
            }

            return CreateActionResult(await _userService.UpdateRoleAsync(request));
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResult(await _userService.DeleteAsync(id));
        }

        [HttpPatch("{id:int}/passive")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Passive(int id)
        {
            return CreateActionResult(await _userService.PassiveAsync(id));
        }

        [NonAction]
        private int GetAuthenticatedUserId()
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(userIdString!);
        }
    }
}
