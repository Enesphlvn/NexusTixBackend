using AutoMapper;
using NexusTix.Application.Features.Venues.Create;
using NexusTix.Application.Features.Venues.Dto;
using NexusTix.Application.Features.Venues.Update;
using NexusTix.Domain.Entities;
using NexusTix.Persistence.Repositories;
using System.Net;

namespace NexusTix.Application.Features.Venues
{
    public class VenueService : IVenueService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public VenueService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResult<VenueResponse>> CreateAsync(CreateVenueRequest request)
        {
            var exists = await _unitOfWork.Venues.AnyAsync(x => x.Name.ToLower() == request.Name.ToLower());

            if (exists)
            {
                return ServiceResult<VenueResponse>.Fail($"Mekan adı: {request.Name}. Bu isimde başka bir mekan mevcut", HttpStatusCode.Conflict);
            }

            var districtExists = await _unitOfWork.Districts.AnyAsync(request.DistrictId);

            if (!districtExists)
            {
                return ServiceResult<VenueResponse>.Fail($"ID'si {request.DistrictId} olan ilçe bulunamadı.", HttpStatusCode.BadRequest);
            }

            var newVenue = _mapper.Map<Venue>(request);
            await _unitOfWork.Venues.AddAsync(newVenue);
            await _unitOfWork.SaveChangesAsync();

            var venueAsDto = _mapper.Map<VenueResponse>(newVenue);

            return ServiceResult<VenueResponse>.SuccessAsCreated(venueAsDto, $"api/venues/{newVenue.Id}");
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var venue = await _unitOfWork.Venues.GetByIdAsync(id);

            if (venue == null)
            {
                return ServiceResult.Fail($"ID'si {id} olan mekan bulunamadı.", HttpStatusCode.NotFound);
            }

            _unitOfWork.Venues.Delete(venue);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult<IEnumerable<VenueResponse>>> GetAllVenuesAsync()
        {
            var venues = await _unitOfWork.Venues.GetAllAsync();
            var venuesAsDto = _mapper.Map<IEnumerable<VenueResponse>>(venues);

            return ServiceResult<IEnumerable<VenueResponse>>.Success(venuesAsDto);
        }

        public async Task<ServiceResult<VenueResponse>> GetByIdAsync(int id)
        {
            var venue = await _unitOfWork.Venues.GetByIdAsync(id);

            if (venue == null)
            {
                return ServiceResult<VenueResponse>.Fail($"ID'si {id} olan mekan bulunamadı.", HttpStatusCode.NotFound);
            }

            var venueAsDto = _mapper.Map<VenueResponse>(venue);

            return ServiceResult<VenueResponse>.Success(venueAsDto);
        }

        public async Task<ServiceResult<IEnumerable<VenueResponse>>> GetPagedAllVenuesAsync(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0 || pageSize <= 0)
            {
                return ServiceResult<IEnumerable<VenueResponse>>.Fail("Sayfa numarası ve boyutu sıfırdan büyük olmalıdır.", HttpStatusCode.BadRequest);
            }

            var venues = await _unitOfWork.Venues.GetAllPagedAsync(pageNumber, pageSize);
            var venuesAsDto = _mapper.Map<IEnumerable<VenueResponse>>(venues);

            return ServiceResult<IEnumerable<VenueResponse>>.Success(venuesAsDto);
        }

        public async Task<ServiceResult<VenueAggregateResponse>> GetVenueAggregateAsync(int id)
        {
            var venue = await _unitOfWork.Venues.GetVenueAggregateAsync(id);

            if (venue == null)
            {
                return ServiceResult<VenueAggregateResponse>.Fail($"ID'si {id} olan mekan bulunamadı.", HttpStatusCode.NotFound);
            }

            var venueAsDto = _mapper.Map<VenueAggregateResponse>(venue);

            return ServiceResult<VenueAggregateResponse>.Success(venueAsDto);
        }

        public async Task<ServiceResult<IEnumerable<VenueAggregateResponse>>> GetVenuesAggregateAsync()
        {
            var venues = await _unitOfWork.Venues.GetVenuesAggregateAsync();
            var venuesAsDto = _mapper.Map<IEnumerable<VenueAggregateResponse>>(venues);

            return ServiceResult<IEnumerable<VenueAggregateResponse>>.Success(venuesAsDto);
        }

        public async Task<ServiceResult<IEnumerable<VenueWithEventsResponse>>> GetVenuesWithEventsAsync()
        {
            var venues = await _unitOfWork.Venues.GetVenuesWithEventsAsync();
            var venuesAsDto = _mapper.Map<IEnumerable<VenueWithEventsResponse>>(venues);

            return ServiceResult<IEnumerable<VenueWithEventsResponse>>.Success(venuesAsDto);
        }

        public async Task<ServiceResult<VenueWithEventsResponse>> GetVenueWithEventsAsync(int id)
        {
            var venue = await _unitOfWork.Venues.GetVenueWithEventsAsync(id);

            if (venue == null)
            {
                return ServiceResult<VenueWithEventsResponse>.Fail($"ID'si {id} olan mekan bulunamadı.", HttpStatusCode.NotFound);
            }

            var venueAsDto = _mapper.Map<VenueWithEventsResponse>(venue);

            return ServiceResult<VenueWithEventsResponse>.Success(venueAsDto);
        }

        public async Task<ServiceResult> PassiveAsync(int id)
        {
            var venue = await _unitOfWork.Venues.GetByIdAsync(id);

            if (venue == null)
            {
                return ServiceResult.Fail($"ID'si {id} olan mekan bulunamadı.", HttpStatusCode.NotFound);
            }

            await _unitOfWork.Venues.PassiveAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }

        public async Task<ServiceResult> UpdateAsync(UpdateVenueRequest request)
        {
            var isDuplicateVenue = await _unitOfWork.Venues.AnyAsync(x => x.Name.ToLower() == request.Name.ToLower() && x.Id != request.Id);
            if (isDuplicateVenue)
            {
                return ServiceResult.Fail("Aynı isimde başka bir mekan mevcut.", HttpStatusCode.Conflict);
            }

            var districtExists = await _unitOfWork.Districts.AnyAsync(request.DistrictId);
            if (!districtExists)
            {
                return ServiceResult.Fail($"ID'si {request.DistrictId} olan ilçe bulunamadı.", HttpStatusCode.BadRequest);
            }

            var venue = await _unitOfWork.Venues.GetByIdAsync(request.Id);
            if (venue == null)
            {
                return ServiceResult.Fail($"ID'si {request.Id} olan mekan bulunamadı.", HttpStatusCode.NotFound);
            }

            _mapper.Map(request, venue);

            _unitOfWork.Venues.Update(venue);
            await _unitOfWork.SaveChangesAsync();

            return ServiceResult.Success(HttpStatusCode.NoContent);
        }
    }
}
