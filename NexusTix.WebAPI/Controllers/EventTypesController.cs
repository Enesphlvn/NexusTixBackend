using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusTix.Application;
using NexusTix.Application.Features.EventTypes;
using NexusTix.Application.Features.EventTypes.Create;
using NexusTix.Application.Features.EventTypes.Update;
using System.Net;

namespace NexusTix.WebAPI.Controllers
{
    public class EventTypesController : BaseController
    {
        private readonly IEventTypeService _eventTypeService;

        public EventTypesController(IEventTypeService eventTypeService)
        {
            _eventTypeService = eventTypeService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllEventTypes()
        {
            return CreateActionResult(await _eventTypeService.GetAllEventTypesAsync());
        }

        [HttpGet("paged")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPagedAllEventTypes([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return CreateActionResult(await _eventTypeService.GetPagedAllEventTypesAsync(pageNumber, pageSize));
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEventType(int id)
        {
            return CreateActionResult(await _eventTypeService.GetByIdAsync(id));
        }

        [HttpGet("{id:int}/aggregate")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEventTypeAggregate(int id)
        {
            return CreateActionResult(await _eventTypeService.GetEventTypeAggregateAsync(id));
        }

        [HttpGet("aggregate")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEventTypesAggregate()
        {
            return CreateActionResult(await _eventTypeService.GetEventTypesAggregateAsync());
        }

        [HttpGet("{id:int}/events")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEventTypeWithEvents(int id)
        {
            return CreateActionResult(await _eventTypeService.GetEventTypeWithEventsAsync(id));
        }

        [HttpGet("events")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEventTypesWithEvents()
        {
            return CreateActionResult(await _eventTypeService.GetEventTypesWithEventsAsync());
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Create([FromBody] CreateEventTypeRequest request)
        {
            return CreateActionResult(await _eventTypeService.CreateAsync(request));
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEventTypeRequest request)
        {
            if (id != request.Id)
            {
                return CreateActionResult(ServiceResult.Fail("Route ID ve Request Body ID eşleşmiyor.", HttpStatusCode.BadRequest));
            }

            return CreateActionResult(await _eventTypeService.UpdateAsync(request));
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResult(await _eventTypeService.DeleteAsync(id));
        }

        [HttpPatch("{id:int}/passive")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Passive(int id)
        {
            return CreateActionResult(await _eventTypeService.PassiveAsync(id));
        }
    }
}
