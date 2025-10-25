using AutoMapper;
using NexusTix.Application.Features.Cities.Create;
using NexusTix.Application.Features.Cities.Dto;
using NexusTix.Application.Features.Cities.Update;
using NexusTix.Domain.Entities;
using NexusTix.Persistence.Repositories;
using System.Net;

namespace NexusTix.Application.Features.Cities
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CityService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResult<CityResponse>> CreateAsync(CreateCityRequest request)
        {
            var exists = await _unitOfWork.Cities.AnyAsync(x => x.Name.ToLower() == request.Name.ToLower());

            if (exists)
            {
                return ServiceResult<CityResponse>.Fail($"Şehir adı: {request.Name}. Bu isimde başka bir şehir mevcut", HttpStatusCode.Conflict);
            }

            var newCity = _mapper.Map<City>(request);

            await _unitOfWork.Cities.AddAsync(newCity);
            await _unitOfWork.SaveChangesAsync();

            var cityDto = _mapper.Map<CityResponse>(newCity);

            return ServiceResult<CityResponse>.SuccessAsCreated(cityDto, $"api/cities/{newCity.Id}");
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var city = await _unitOfWork.Cities.GetByIdAsync(id);

            if (city == null)
            {
                return ServiceResult.Fail($"ID'si {id} olan şehir bulunamadı.", HttpStatusCode.NotFound);
            }

            _unitOfWork.Cities.Delete(city);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult<IEnumerable<CityResponse>>> GetAllCitiesAsync()
        {
            var cities = await _unitOfWork.Cities.GetAllAsync();
            var citiesAsDto = _mapper.Map<IEnumerable<CityResponse>>(cities);

            return ServiceResult<IEnumerable<CityResponse>>.Success(citiesAsDto);
        }

        public async Task<ServiceResult<CityResponse>> GetByIdAsync(int id)
        {
            var city = await _unitOfWork.Cities.GetByIdAsync(id);

            if (city == null)
            {
                return ServiceResult<CityResponse>.Fail($"ID'si {id} olan şehir bulunamadı.", HttpStatusCode.NotFound);
            }

            var cityAsDto = _mapper.Map<CityResponse>(city);

            return ServiceResult<CityResponse>.Success(cityAsDto);
        }

        public async Task<ServiceResult<IEnumerable<CityWithDistrictsAndVenuesResponse>>> GetCitiesWithDistrictsAndVenuesAsync()
        {
            var cities = await _unitOfWork.Cities.GetCitiesWithDistrictsAndVenuesAsync();

            var citiesAsDto = _mapper.Map<IEnumerable<CityWithDistrictsAndVenuesResponse>>(cities);

            return ServiceResult<IEnumerable<CityWithDistrictsAndVenuesResponse>>.Success(citiesAsDto);
        }

        public async Task<ServiceResult<IEnumerable<CityWithDistrictsResponse>>> GetCitiesWithDistrictsAsync()
        {
            var cities = await _unitOfWork.Cities.GetCitiesWithDistrictsAsync();

            var citiesAsDto = _mapper.Map<IEnumerable<CityWithDistrictsResponse>>(cities);

            return ServiceResult<IEnumerable<CityWithDistrictsResponse>>.Success(citiesAsDto);
        }

        public async Task<ServiceResult<IEnumerable<CityWithVenuesResponse>>> GetCitiesWithVenuesAsync()
        {
            var cities = await _unitOfWork.Cities.GetCitiesWithVenuesAsync();

            var citiesAsDto = _mapper.Map<IEnumerable<CityWithVenuesResponse>>(cities);

            return ServiceResult<IEnumerable<CityWithVenuesResponse>>.Success(citiesAsDto);
        }

        public async Task<ServiceResult<CityWithDistrictsAndVenuesResponse>> GetCityWithDistrictsAndVenuesAsync(int id)
        {
            var city = await _unitOfWork.Cities.GetCityWithDistrictsAndVenuesAsync(id);

            if (city == null)
            {
                return ServiceResult<CityWithDistrictsAndVenuesResponse>.Fail($"ID'si {id} olan şehir bulunamadı.", HttpStatusCode.NotFound);
            }

            var cityAsDto = _mapper.Map<CityWithDistrictsAndVenuesResponse>(city);

            return ServiceResult<CityWithDistrictsAndVenuesResponse>.Success(cityAsDto);
        }

        public async Task<ServiceResult<CityWithDistrictsResponse>> GetCityWithDistrictsAsync(int id)
        {
            var city = await _unitOfWork.Cities.GetCityWithDistrictsAsync(id);

            if (city == null)
            {
                return ServiceResult<CityWithDistrictsResponse>.Fail($"ID'si {id} olan şehir bulunamadı.", HttpStatusCode.NotFound);
            }

            var cityAsDto = _mapper.Map<CityWithDistrictsResponse>(city);

            return ServiceResult<CityWithDistrictsResponse>.Success(cityAsDto);
        }

        public async Task<ServiceResult<CityWithVenuesResponse>> GetCityWithVenuesAsync(int id)
        {
            var city = await _unitOfWork.Cities.GetCityWithVenuesAsync(id);

            if (city == null)
            {
                return ServiceResult<CityWithVenuesResponse>.Fail($"ID'si {id} olan şehir bulunamadı.", HttpStatusCode.NotFound);
            }

            var cityAsDto = _mapper.Map<CityWithVenuesResponse>(city);

            return ServiceResult<CityWithVenuesResponse>.Success(cityAsDto);
        }

        public async Task<ServiceResult<IEnumerable<CityResponse>>> GetPagedAllCitiesAsync(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return ServiceResult<IEnumerable<CityResponse>>.Fail("Geçersiz sayı", HttpStatusCode.BadRequest);
            }

            var cities = await _unitOfWork.Cities.GetAllPagedAsync(pageNumber, pageSize);

            var citiesAsDto = _mapper.Map<IEnumerable<CityResponse>>(cities);

            return ServiceResult<IEnumerable<CityResponse>>.Success(citiesAsDto);
        }

        public async Task<ServiceResult> PassiveAsync(int id)
        {
            var city = await _unitOfWork.Cities.GetByIdAsync(id);

            if (city == null)
            {
                return ServiceResult.Fail($"ID'si {id} olan şehir bulunamadı.", HttpStatusCode.NotFound);
            }

            await _unitOfWork.Cities.PassiveAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> UpdateAsync(UpdateCityRequest request)
        {
            var isDuplicateCity = await _unitOfWork.Cities.AnyAsync(x => x.Name.ToLower() == request.Name.ToLower() && x.Id != request.Id);

            if (isDuplicateCity)
            {
                return ServiceResult.Fail("Aynı isimde başka bir şehir mevcut.", HttpStatusCode.Conflict);
            }

            var city = await _unitOfWork.Cities.GetByIdAsync(request.Id);

            if (city == null)
            {
                return ServiceResult.Fail($"ID'si {request.Id} olan şehir bulunamadı.", HttpStatusCode.NotFound);
            }

            _mapper.Map(request, city);

            _unitOfWork.Cities.Update(city);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
