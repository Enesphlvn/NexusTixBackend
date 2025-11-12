using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusTix.Application;
using NexusTix.Application.Features.Districts;
using NexusTix.Application.Features.Districts.Create;
using NexusTix.Application.Features.Districts.Update;
using System.Net;

namespace NexusTix.WebAPI.Controllers
{
    public class DistrictsController : BaseController
    {
        private readonly IDistrictService _districtService;

        public DistrictsController(IDistrictService districtService)
        {
            _districtService = districtService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllDistricts()
        {
            return CreateActionResult(await _districtService.GetAllDistrictsAsync());
        }

        [HttpGet("paged")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPagedAllDistricts([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return CreateActionResult(await _districtService.GetPagedAllDistrictsAsync(pageNumber, pageSize));
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDistrict(int id)
        {
            return CreateActionResult(await _districtService.GetByIdAsync(id));
        }

        [HttpGet("{id:int}/aggregate")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDistrictAggregate(int id)
        {
            return CreateActionResult(await _districtService.GetDistrictAggregateAsync(id));
        }

        [HttpGet("aggregate")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDistrictsAggregate()
        {
            return CreateActionResult(await _districtService.GetDistrictsAggregateAsync());

        }

        [HttpGet("{id:int}/venues")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDistrictWithVenues(int id)
        {
            return CreateActionResult(await _districtService.GetDistrictWithVenuesAsync(id));
        }

        [HttpGet("venues")]
        [AllowAnonymous]
        public async Task<IActionResult> GetDistrictsWithVenues()
        {
            return CreateActionResult(await _districtService.GetDistrictsWithVenuesAsync());

        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Create([FromBody] CreateDistrictRequest request)
        {
            return CreateActionResult(await _districtService.CreateAsync(request));
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDistrictRequest request)
        {
            if (id != request.Id)
            {
                return CreateActionResult(ServiceResult.Fail("Route ID ve Request Body ID eşleşmiyor.", HttpStatusCode.BadRequest));
            }
            return CreateActionResult(await _districtService.UpdateAsync(request));
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResult(await _districtService.DeleteAsync(id));
        }

        [HttpPatch("{id:int}/passive")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Passive(int id)
        {
            return CreateActionResult(await _districtService.PassiveAsync(id));
        }
    }
}
