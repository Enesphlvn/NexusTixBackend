using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexusTix.Application;
using NexusTix.Application.Features.Cities;
using NexusTix.Application.Features.Cities.Create;
using NexusTix.Application.Features.Cities.Update;
using System.Net;

namespace NexusTix.WebAPI.Controllers
{
    public class CitiesController : BaseController
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllCities()
        {
            return CreateActionResult(await _cityService.GetAllCitiesAsync());
        }

        [HttpGet("paged")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPagedAllCities([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return CreateActionResult(await _cityService.GetPagedAllCitiesAsync(pageNumber, pageSize));
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCity(int id)
        {
            return CreateActionResult(await _cityService.GetByIdAsync(id));
        }

        [HttpGet("aggregate/{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCityAggregate(int id)
        {
            return CreateActionResult(await _cityService.GetCityAggregateAsync(id));
        }

        [HttpGet("aggregate")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCitiesAggregate()
        {
            return CreateActionResult(await _cityService.GetCitiesAggregateAsync());
        }

        [HttpGet("{id:int}/districts")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCityWithDistricts(int id)
        {
            return CreateActionResult(await _cityService.GetCityWithDistrictsAsync(id));
        }

        [HttpGet("districts")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCitiesWithDistricts()
        {
            return CreateActionResult(await _cityService.GetCitiesWithDistrictsAsync());
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Create([FromBody] CreateCityRequest request)
        {
            return CreateActionResult(await _cityService.CreateAsync(request));
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCityRequest request)
        {
            if (id != request.Id)
            {
                return CreateActionResult(ServiceResult.Fail("Route ID ve Request Body ID eşleşmiyor.", HttpStatusCode.BadRequest));
            }
            return CreateActionResult(await _cityService.UpdateAsync(request));
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResult(await _cityService.DeleteAsync(id));
        }

        [HttpPatch("{id:int}/passive")]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Passive(int id)
        {
            return CreateActionResult(await _cityService.PassiveAsync(id));
        }
    }
}
