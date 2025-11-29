using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusTix.Application;
using NexusTix.Application.Features.Venues;
using NexusTix.Application.Features.Venues.Create;
using NexusTix.Application.Features.Venues.Update;
using System.Net;

namespace NexusTix.WebAPI.Controllers
{
    public class VenuesController : BaseController
    {
        private readonly IVenueService _venueService;

        public VenuesController(IVenueService venueService)
        {
            _venueService = venueService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllVenues()
        {
            return CreateActionResult(await _venueService.GetAllVenuesAsync());
        }

        [HttpGet("paged")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPagedAllVenues([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return CreateActionResult(await _venueService.GetPagedAllVenuesAsync(pageNumber, pageSize));
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetVenue(int id)
        {
            return CreateActionResult(await _venueService.GetByIdAsync(id));
        }

        [HttpGet("admin-list")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllVenuesForAdmin()
        {
            return CreateActionResult(await _venueService.GetAllVenuesForAdminAsync());
        }

        [HttpGet("{id:int}/admin-edit")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> GetVenueForAdmin(int id)
        {
            return CreateActionResult(await _venueService.GetVenueForAdminAsync(id));
        }

        [HttpGet("{id:int}/events")]
        [AllowAnonymous]
        public async Task<IActionResult> GetVenueWithEvents(int id)
        {
            return CreateActionResult(await _venueService.GetVenueWithEventsAsync(id));
        }

        [HttpGet("events")]
        [AllowAnonymous]
        public async Task<IActionResult> GetVenuesWithEvents()
        {
            return CreateActionResult(await _venueService.GetVenuesWithEventsAsync());
        }

        [HttpGet("{id:int}/aggregate")]
        [AllowAnonymous]
        public async Task<IActionResult> GetVenueAggregate(int id)
        {
            return CreateActionResult(await _venueService.GetVenueAggregateAsync(id));
        }

        [HttpGet("aggregate")]
        [AllowAnonymous]
        public async Task<IActionResult> GetVenuesAggregate()
        {
            return CreateActionResult(await _venueService.GetVenuesAggregateAsync());
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Create([FromBody] CreateVenueRequest request)
        {
            return CreateActionResult(await _venueService.CreateAsync(request));
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateVenueRequest request)
        {
            if (id != request.Id)
            {
                return CreateActionResult(ServiceResult.Fail("Route ID ve Request Body ID eşleşmiyor.", HttpStatusCode.BadRequest));
            }

            return CreateActionResult(await _venueService.UpdateAsync(request));
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResult(await _venueService.DeleteAsync(id));
        }

        [HttpPatch("{id:int}/passive")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Passive(int id)
        {
            return CreateActionResult(await _venueService.PassiveAsync(id));
        }
    }
}
