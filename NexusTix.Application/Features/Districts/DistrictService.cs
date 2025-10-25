using AutoMapper;
using NexusTix.Application.Features.Cities.Dto;
using NexusTix.Application.Features.Districts.Create;
using NexusTix.Application.Features.Districts.Dto;
using NexusTix.Application.Features.Districts.Update;
using NexusTix.Domain.Entities;
using NexusTix.Persistence.Repositories;
using System.Net;

namespace NexusTix.Application.Features.Districts
{
    public class DistrictService : IDistrictService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DistrictService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResult<DistrictResponse>> CreateAsync(CreateDistrictRequest request)
        {
            var exists = await _unitOfWork.Districts.AnyAsync(x => x.Name.ToLower() == request.Name.ToLower());

            if (exists)
            {
                return ServiceResult<DistrictResponse>.Fail($"İlçe adı: {request.Name}. Bu isimde başka bir ilçe mevcut", HttpStatusCode.Conflict);
            }

            var cityExists = await _unitOfWork.Cities.AnyAsync(request.CityId);

            if (!cityExists)
            {
                return ServiceResult<DistrictResponse>.Fail($"ID'si {request.CityId} olan şehir bulunamadı.", HttpStatusCode.BadRequest);
            }

            var newDistrict = _mapper.Map<District>(request);

            await _unitOfWork.Districts.AddAsync(newDistrict);
            await _unitOfWork.SaveChangesAsync();

            var districtAsDto = _mapper.Map<DistrictResponse>(newDistrict);

            return ServiceResult<DistrictResponse>.SuccessAsCreated(districtAsDto, $"api/districts/{newDistrict.Id}");
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var district = await _unitOfWork.Districts.GetByIdAsync(id);

            if (district == null)
            {
                return ServiceResult.Fail($"ID'si {id} olan ilçe bulunamadı.", HttpStatusCode.NotFound);
            }

            _unitOfWork.Districts.Delete(district);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult<IEnumerable<DistrictResponse>>> GetAllDistrictsAsync()
        {
            var districts = await _unitOfWork.Districts.GetAllAsync();
            var districtsAsDto = _mapper.Map<IEnumerable<DistrictResponse>>(districts);

            return ServiceResult<IEnumerable<DistrictResponse>>.Success(districtsAsDto);
        }

        public async Task<ServiceResult<DistrictResponse>> GetByIdAsync(int id)
        {
            var district = await _unitOfWork.Districts.GetByIdAsync(id);

            if (district == null)
            {
                return ServiceResult<DistrictResponse>.Fail($"ID'si {id} olan ilçe bulunamadı.", HttpStatusCode.NotFound);
            }

            var districtAsDto = _mapper.Map<DistrictResponse>(district);

            return ServiceResult<DistrictResponse>.Success(districtAsDto);
        }

        public async Task<ServiceResult<IEnumerable<DistrictWithVenuesResponse>>> GetDistrictsWithVenuesAsync()
        {
            var districts = await _unitOfWork.Districts.GetDistrictsWithVenuesAsync();
            var districtsAsDto = _mapper.Map<IEnumerable<DistrictWithVenuesResponse>>(districts);

            return ServiceResult<IEnumerable<DistrictWithVenuesResponse>>.Success(districtsAsDto);
        }

        public async Task<ServiceResult<DistrictWithVenuesResponse>> GetDistrictWithVenuesAsync(int id)
        {
            var district = await _unitOfWork.Districts.GetDistrictWithVenuesAsync(id);

            if (district == null)
            {
                return ServiceResult<DistrictWithVenuesResponse>.Fail($"ID'si {id} olan ilçe bulunamadı.", HttpStatusCode.NotFound);
            }

            var districtAsDto = _mapper.Map<DistrictWithVenuesResponse>(district);

            return ServiceResult<DistrictWithVenuesResponse>.Success(districtAsDto);
        }

        public async Task<ServiceResult<IEnumerable<DistrictResponse>>> GetPagedAllDistrictsAsync(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return ServiceResult<IEnumerable<DistrictResponse>>.Fail("Geçersiz sayı", HttpStatusCode.BadRequest);
            }

            var districts = await _unitOfWork.Districts.GetAllPagedAsync(pageNumber, pageSize);
            var districtsAsDto = _mapper.Map<IEnumerable<DistrictResponse>>(districts);

            return ServiceResult<IEnumerable<DistrictResponse>>.Success(districtsAsDto);
        }

        public async Task<ServiceResult> PassiveAsync(int id)
        {
            var district = await _unitOfWork.Districts.GetByIdAsync(id);

            if (district == null)
            {
                return ServiceResult.Fail($"ID'si {id} olan ilçe bulunamadı.", HttpStatusCode.NotFound);
            }

            await _unitOfWork.Districts.PassiveAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> UpdateAsync(UpdateDistrictRequest request)
        {
            var isDublicateDistrict = await _unitOfWork.Districts.AnyAsync(x => x.Name.ToLower() == request.Name.ToLower() && x.Id != request.Id);

            if (isDublicateDistrict)
            {
                return ServiceResult.Fail("Aynı isimde başka bir ilçe mevcut.", HttpStatusCode.Conflict);
            }

            var cityExists = await _unitOfWork.Cities.AnyAsync(request.CityId);

            if (!cityExists)
            {
                return ServiceResult.Fail($"ID'si {request.CityId} olan şehir bulunamadı.", HttpStatusCode.BadRequest);
            }

            var district = await _unitOfWork.Districts.GetByIdAsync(request.Id);

            if (district == null)
            {
                return ServiceResult.Fail($"ID'si {request.Id} olan ilçe bulunamadı.", HttpStatusCode.NotFound);
            }

            _mapper.Map(request, district);

            _unitOfWork.Districts.Update(district);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
