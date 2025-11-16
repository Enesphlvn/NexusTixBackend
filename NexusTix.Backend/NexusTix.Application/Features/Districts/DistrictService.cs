using AutoMapper;
using NexusTix.Application.Features.Districts.Create;
using NexusTix.Application.Features.Districts.Responses;
using NexusTix.Application.Features.Districts.Rules;
using NexusTix.Application.Features.Districts.Update;
using NexusTix.Domain.Entities;
using NexusTix.Domain.Exceptions;
using NexusTix.Persistence.Repositories;
using System.Net;

namespace NexusTix.Application.Features.Districts
{
    public class DistrictService : IDistrictService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDistrictBusinessRules _districtRules;

        public DistrictService(IUnitOfWork unitOfWork, IMapper mapper, IDistrictBusinessRules districtRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _districtRules = districtRules;
        }

        public async Task<ServiceResult<DistrictResponse>> CreateAsync(CreateDistrictRequest request)
        {
            try
            {
                await _districtRules.CheckIfDistrictNameExistsWhenCreating(request.Name);
                await _districtRules.CheckIfCityExists(request.CityId);

                var newDistrict = _mapper.Map<District>(request);

                await _unitOfWork.Districts.AddAsync(newDistrict);
                await _unitOfWork.SaveChangesAsync();

                var districtAsDto = _mapper.Map<DistrictResponse>(newDistrict);

                return ServiceResult<DistrictResponse>.SuccessAsCreated(districtAsDto, $"api/districts/{newDistrict.Id}");
            }
            catch (BusinessException ex)
            {
                return ServiceResult<DistrictResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            try
            {
                await _districtRules.CheckIfDistrictExists(id);
                await _districtRules.CheckIfDistrictHasNoVenues(id);

                var district = await _unitOfWork.Districts.GetByIdAsync(id);

                _unitOfWork.Districts.Delete(district!);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.Success(HttpStatusCode.NoContent);
            }
            catch (BusinessException ex)
            {
                return ServiceResult.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<DistrictResponse>>> GetAllDistrictsAsync()
        {
            try
            {
                var districts = await _unitOfWork.Districts.GetAllAsync();
                var districtsAsDto = _mapper.Map<IEnumerable<DistrictResponse>>(districts);

                return ServiceResult<IEnumerable<DistrictResponse>>.Success(districtsAsDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<DistrictResponse>>.Fail($"Bir hata oluştu: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResult<DistrictResponse>> GetByIdAsync(int id)
        {
            try
            {
                await _districtRules.CheckIfDistrictExists(id);

                var district = await _unitOfWork.Districts.GetByIdAsync(id);

                var districtAsDto = _mapper.Map<DistrictResponse>(district);

                return ServiceResult<DistrictResponse>.Success(districtAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<DistrictResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<DistrictAggregateResponse>> GetDistrictAggregateAsync(int id)
        {
            try
            {
                await _districtRules.CheckIfDistrictExists(id);

                var district = await _unitOfWork.Districts.GetDistrictAggregateAsync(id);

                var districtAsDto = _mapper.Map<DistrictAggregateResponse>(district);

                return ServiceResult<DistrictAggregateResponse>.Success(districtAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<DistrictAggregateResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<DistrictAggregateResponse>>> GetDistrictsAggregateAsync()
        {
            try
            {
                var districts = await _unitOfWork.Districts.GetDistrictsAggregateAsync();

                var districtsAsDto = _mapper.Map<IEnumerable<DistrictAggregateResponse>>(districts);

                return ServiceResult<IEnumerable<DistrictAggregateResponse>>.Success(districtsAsDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<DistrictAggregateResponse>>.Fail($"Bir hata oluştu: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResult<IEnumerable<DistrictResponse>>> GetDistrictsByCityAsync(int cityId)
        {
            try
            {
                await _districtRules.CheckIfCityExists(cityId);

                var districts = await _unitOfWork.Districts.GetDistrictsByCityAsync(cityId);

                var districtsAsDto = _mapper.Map<IEnumerable<DistrictResponse>>(districts);

                return ServiceResult<IEnumerable<DistrictResponse>>.Success(districtsAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<IEnumerable<DistrictResponse>>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<DistrictWithVenuesResponse>>> GetDistrictsWithVenuesAsync()
        {
            try
            {
                var districts = await _unitOfWork.Districts.GetDistrictsWithVenuesAsync();
                var districtsAsDto = _mapper.Map<IEnumerable<DistrictWithVenuesResponse>>(districts);

                return ServiceResult<IEnumerable<DistrictWithVenuesResponse>>.Success(districtsAsDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<DistrictWithVenuesResponse>>.Fail($"Bir hata oluştu: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResult<DistrictWithVenuesResponse>> GetDistrictWithVenuesAsync(int id)
        {
            try
            {
                await _districtRules.CheckIfDistrictExists(id);

                var district = await _unitOfWork.Districts.GetDistrictWithVenuesAsync(id);

                var districtAsDto = _mapper.Map<DistrictWithVenuesResponse>(district);

                return ServiceResult<DistrictWithVenuesResponse>.Success(districtAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<DistrictWithVenuesResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<DistrictResponse>>> GetPagedAllDistrictsAsync(int pageNumber, int pageSize)
        {
            try
            {
                _districtRules.CheckIfPagingParametersAreValid(pageNumber, pageSize);

                var districts = await _unitOfWork.Districts.GetAllPagedAsync(pageNumber, pageSize);
                var districtsAsDto = _mapper.Map<IEnumerable<DistrictResponse>>(districts);

                return ServiceResult<IEnumerable<DistrictResponse>>.Success(districtsAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<IEnumerable<DistrictResponse>>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult> PassiveAsync(int id)
        {
            try
            {
                await _districtRules.CheckIfDistrictExists(id);
                await _districtRules.CheckIfDistrictHasNoVenues(id);

                await _unitOfWork.Districts.PassiveAsync(id);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.Success(HttpStatusCode.NoContent);
            }
            catch (BusinessException ex)
            {
                return ServiceResult.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult> UpdateAsync(UpdateDistrictRequest request)
        {
            try
            {
                await _districtRules.CheckIfDistrictNameExistsWhenUpdating(request.Id, request.Name);
                await _districtRules.CheckIfCityExists(request.CityId);
                await _districtRules.CheckIfDistrictExists(request.Id);

                var district = await _unitOfWork.Districts.GetByIdAsync(request.Id);

                _mapper.Map(request, district);

                _unitOfWork.Districts.Update(district!);
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
