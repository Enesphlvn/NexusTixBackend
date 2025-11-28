using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusTix.Application;
using NexusTix.Application.Features.Events;
using NexusTix.Application.Features.Events.Create;
using NexusTix.Application.Features.Events.Update;
using System.Net;

namespace NexusTix.WebAPI.Controllers
{
    public class EventsController : BaseController
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllEvents()
        {
            return CreateActionResult(await _eventService.GetAllEventsAsync());
        }

        [HttpGet("paged")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPagedAllEvents([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return CreateActionResult(await _eventService.GetPagedAllEventsAsync(pageNumber, pageSize));
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEvent(int id)
        {
            return CreateActionResult(await _eventService.GetByIdAsync(id));
        }

        [HttpGet("admin-list")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetEventsForAdmin()
        {
            return CreateActionResult(await _eventService.GetEventsForAdminAsync());
        }

        [HttpGet("{id:int}/aggregate")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEventAggregate(int id)
        {
            return CreateActionResult(await _eventService.GetEventAggregateAsync(id));
        }

        [HttpGet("aggregate")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEventsAggregate()
        {
            return CreateActionResult(await _eventService.GetEventsAggregateAsync());
        }

        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFilteredEvents([FromQuery] int? cityId, [FromQuery] int? eventTypeId, [FromQuery] int? districtId, [FromQuery] int? artistId, [FromQuery] DateTimeOffset? date)
        {
            return CreateActionResult(await _eventService.GetFilteredEventsAsync(cityId, districtId, eventTypeId, artistId, date));
        }

        [HttpGet("{eventTypeId:int}/eventtype")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEventsByEventType(int eventTypeId)
        {
            return CreateActionResult(await _eventService.GetEventsByEventTypeAsync(eventTypeId));
        }

        [HttpGet("{venueId:int}/venue")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEventsByVenue(int venueId)
        {
            return CreateActionResult(await _eventService.GetEventsByVenueAsync(venueId));
        }

        [HttpGet("{userId:int}/usertickets")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEventsByUserTickets(int userId)
        {
            return CreateActionResult(await _eventService.GetEventsByUserTicketsAsync(userId));
        }

        [HttpGet("daterange")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEventsByDateRange([FromQuery] DateTimeOffset startDate, [FromQuery] DateTimeOffset endDate)
        {
            return CreateActionResult(await _eventService.GetEventsByDateRangeAsync(startDate, endDate));
        }

        [HttpGet("pricerange")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEventsByPriceRange([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice)
        {
            return CreateActionResult(await _eventService.GetEventsByPriceRangeAsync(minPrice, maxPrice));
        }

        [HttpGet("highestsales")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEventsWithHighestSales([FromQuery] int numberOfEvents)
        {
            return CreateActionResult(await _eventService.GetEventsWithHighestSalesAsync(numberOfEvents));
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Create([FromBody] CreateEventRequest request)
        {
            return CreateActionResult(await _eventService.CreateAsync(request));
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEventRequest request)
        {
            if (id != request.Id)
            {
                return CreateActionResult(ServiceResult.Fail("Route ID ve Request Body ID eşleşmiyor.", HttpStatusCode.BadRequest));
            }

            return CreateActionResult(await _eventService.UpdateAsync(request));
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResult(await _eventService.DeleteAsync(id));
        }

        [HttpPatch("{id:int}/passive")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Passive(int id)
        {
            return CreateActionResult(await _eventService.PassiveAsync(id));
        }
    }
}
