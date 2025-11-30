using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusTix.Application;
using NexusTix.Application.Features.Artists;
using NexusTix.Application.Features.Artists.Create;
using NexusTix.Application.Features.Artists.Update;
using System.Net;

namespace NexusTix.WebAPI.Controllers
{
    public class ArtistsController : BaseController
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllArtists()
        {
            return CreateActionResult(await _artistService.GetAllArtistsAsync());
        }

        [HttpGet("paged")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPagedAllArtists([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return CreateActionResult(await _artistService.GetPagedAllArtistsAsync(pageNumber, pageSize));
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetArtist(int id)
        {
            return CreateActionResult(await _artistService.GetByIdAsync(id));
        }

        [HttpGet("admin-list")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllArtistsForAdmin()
        {
            return CreateActionResult(await _artistService.GetAllArtistsForAdminAsync());
        }

        [HttpGet("{id:int}/admin-edit")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> GetArtistForAdmin(int id)
        {
            return CreateActionResult(await _artistService.GetArtistForAdminAsync(id));
        }

        [HttpGet("{id:int}/events")]
        [AllowAnonymous]
        public async Task<IActionResult> GetArtistWithEvents(int id)
        {
            return CreateActionResult(await _artistService.GetArtistWithEventsAsync(id));
        }

        [HttpGet("events")]
        [AllowAnonymous]
        public async Task<IActionResult> GetArtistsWithEvents()
        {
            return CreateActionResult(await _artistService.GetArtistsWithEventsAsync());
        }

        [HttpGet("by-eventtype/{eventTypeId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetArtistsByEventType(int eventTypeId)
        {
            return CreateActionResult(await _artistService.GetArtistsByEventTypeAsync(eventTypeId));
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Create([FromBody] CreateArtistRequest request)
        {
            return CreateActionResult(await _artistService.CreateAsync(request));
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateArtistRequest request)
        {
            if (id != request.Id)
            {
                return CreateActionResult(ServiceResult.Fail("Route ID ve Request Body ID eşleşmiyor.", HttpStatusCode.BadRequest));
            }
            return CreateActionResult(await _artistService.UpdateAsync(request));
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResult(await _artistService.DeleteAsync(id));
        }

        [HttpPatch("{id:int}/passive")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Passive(int id)
        {
            return CreateActionResult(await _artistService.PassiveAsync(id));
        }
    }
}
