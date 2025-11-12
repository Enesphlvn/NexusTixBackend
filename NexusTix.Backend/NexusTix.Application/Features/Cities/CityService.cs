using AutoMapper;
using NexusTix.Application.Features.Cities.Create;
using NexusTix.Application.Features.Cities.Responses;
using NexusTix.Application.Features.Cities.Rules;
using NexusTix.Application.Features.Cities.Update;
using NexusTix.Domain.Entities;
using NexusTix.Domain.Exceptions;
using NexusTix.Persistence.Repositories;
using System.Net;

namespace NexusTix.Application.Features.Cities
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICityBusinessRules _cityRules;

        public CityService(IUnitOfWork unitOfWork, IMapper mapper, ICityBusinessRules cityRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cityRules = cityRules;
        }

        public async Task<ServiceResult<CityResponse>> CreateAsync(CreateCityRequest request)
        {
            try
            {
                await _cityRules.CheckIfCityNameExistsWhenCreating(request.Name);

                var newCity = _mapper.Map<City>(request);

                await _unitOfWork.Cities.AddAsync(newCity);
                await _unitOfWork.SaveChangesAsync();

                var cityDto = _mapper.Map<CityResponse>(newCity);

                return ServiceResult<CityResponse>.SuccessAsCreated(cityDto, $"api/cities/{newCity.Id}");
            }
            catch (BusinessException ex)
            {
                return ServiceResult<CityResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            try
            {
                await _cityRules.CheckIfCityExists(id);
                await _cityRules.CheckIfCityHasNoDistricts(id);

                var city = await _unitOfWork.Cities.GetByIdAsync(id);

                _unitOfWork.Cities.Delete(city!);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.Success(HttpStatusCode.NoContent);
            }
            catch (BusinessException ex)
            {
                return ServiceResult.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<CityResponse>>> GetAllCitiesAsync()
        {
            var cities = await _unitOfWork.Cities.GetAllAsync();
            var citiesAsDto = _mapper.Map<IEnumerable<CityResponse>>(cities);

            return ServiceResult<IEnumerable<CityResponse>>.Success(citiesAsDto);
        }

        public async Task<ServiceResult<CityResponse>> GetByIdAsync(int id)
        {
            try
            {
                await _cityRules.CheckIfCityExists(id);

                var city = await _unitOfWork.Cities.GetByIdAsync(id);

                var cityAsDto = _mapper.Map<CityResponse>(city);

                return ServiceResult<CityResponse>.Success(cityAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<CityResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<CityAggregateResponse>>> GetCitiesAggregateAsync()
        {
            var cities = await _unitOfWork.Cities.GetCitiesAggregateAsync();
            var citiesAsDto = _mapper.Map<IEnumerable<CityAggregateResponse>>(cities);

            return ServiceResult<IEnumerable<CityAggregateResponse>>.Success(citiesAsDto);
        }

        public async Task<ServiceResult<IEnumerable<CityWithDistrictsResponse>>> GetCitiesWithDistrictsAsync()
        {
            var cities = await _unitOfWork.Cities.GetCitiesWithDistrictsAsync();

            var citiesAsDto = _mapper.Map<IEnumerable<CityWithDistrictsResponse>>(cities);

            return ServiceResult<IEnumerable<CityWithDistrictsResponse>>.Success(citiesAsDto);
        }

        public async Task<ServiceResult<CityAggregateResponse>> GetCityAggregateAsync(int id)
        {
            try
            {
                await _cityRules.CheckIfCityExists(id);

                var city = await _unitOfWork.Cities.GetCityAggregateAsync(id);

                var cityAsDto = _mapper.Map<CityAggregateResponse>(city);

                return ServiceResult<CityAggregateResponse>.Success(cityAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<CityAggregateResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<CityWithDistrictsResponse>> GetCityWithDistrictsAsync(int id)
        {
            try
            {
                await _cityRules.CheckIfCityExists(id);

                var city = await _unitOfWork.Cities.GetCityWithDistrictsAsync(id);

                var cityAsDto = _mapper.Map<CityWithDistrictsResponse>(city);

                return ServiceResult<CityWithDistrictsResponse>.Success(cityAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<CityWithDistrictsResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<CityResponse>>> GetPagedAllCitiesAsync(int pageNumber, int pageSize)
        {
            try
            {
                _cityRules.CheckIfPagingParametersAreValid(pageNumber, pageSize);

                var cities = await _unitOfWork.Cities.GetAllPagedAsync(pageNumber, pageSize);

                var citiesAsDto = _mapper.Map<IEnumerable<CityResponse>>(cities);

                return ServiceResult<IEnumerable<CityResponse>>.Success(citiesAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<IEnumerable<CityResponse>>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult> PassiveAsync(int id)
        {
            try
            {
                await _cityRules.CheckIfCityExists(id);
                await _cityRules.CheckIfCityHasNoDistricts(id);

                await _unitOfWork.Cities.PassiveAsync(id);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.Success(HttpStatusCode.NoContent);
            }
            catch (BusinessException ex)
            {
                return ServiceResult.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult> UpdateAsync(UpdateCityRequest request)
        {
            try
            {
                await _cityRules.CheckIfCityNameExistsWhenUpdating(request.Id, request.Name);
                await _cityRules.CheckIfCityExists(request.Id);

                var city = await _unitOfWork.Cities.GetByIdAsync(request.Id);

                _mapper.Map(request, city);

                _unitOfWork.Cities.Update(city!);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.Success(HttpStatusCode.NoContent);
            }
            catch (BusinessException ex)
            {
                return ServiceResult.Fail(ex.Message, ex.StatusCode);
            }
        }
    }
}
