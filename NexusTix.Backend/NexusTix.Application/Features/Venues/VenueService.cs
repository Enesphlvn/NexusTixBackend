using AutoMapper;
using NexusTix.Application.Features.Events.Responses;
using NexusTix.Application.Features.Venues.Create;
using NexusTix.Application.Features.Venues.Responses;
using NexusTix.Application.Features.Venues.Rules;
using NexusTix.Application.Features.Venues.Update;
using NexusTix.Domain.Entities;
using NexusTix.Domain.Exceptions;
using NexusTix.Persistence.Repositories;
using System.Net;

namespace NexusTix.Application.Features.Venues
{
    public class VenueService : IVenueService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IVenueBusinessRules _venueRules;

        public VenueService(IUnitOfWork unitOfWork, IMapper mapper, IVenueBusinessRules venueRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _venueRules = venueRules;
        }

        public async Task<ServiceResult<VenueResponse>> CreateAsync(CreateVenueRequest request)
        {
            try
            {
                await _venueRules.CheckIfVenueNameExistsWhenCreating(request.Name);
                await _venueRules.CheckIfDistrictExists(request.DistrictId);

                var newVenue = _mapper.Map<Venue>(request);
                await _unitOfWork.Venues.AddAsync(newVenue);
                await _unitOfWork.SaveChangesAsync();

                var venueAsDto = _mapper.Map<VenueResponse>(newVenue);
                return ServiceResult<VenueResponse>.SuccessAsCreated(venueAsDto, $"api/venues/{newVenue.Id}");
            }
            catch (BusinessException ex)
            {
                return ServiceResult<VenueResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            try
            {
                await _venueRules.CheckIfVenueExists(id);
                await _venueRules.CheckIfVenueHasActiveEvents(id);

                var venue = await _unitOfWork.Venues.GetByIdAsync(id);
                _unitOfWork.Venues.Delete(venue!);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.Success(HttpStatusCode.NoContent);
            }
            catch (BusinessException ex)
            {
                return ServiceResult.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<VenueResponse>>> GetAllVenuesAsync()
        {
            try
            {
                var venues = await _unitOfWork.Venues.GetAllAsync();
                var venuesAsDto = _mapper.Map<IEnumerable<VenueResponse>>(venues);

                return ServiceResult<IEnumerable<VenueResponse>>.Success(venuesAsDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<VenueResponse>>.Fail($"Bir hata oluştu: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResult<IEnumerable<VenueAdminResponse>>> GetAllVenuesForAdminAsync()
        {
            try
            {
                var venues = await _unitOfWork.Venues.GetAllVenuesForAdminAsync();

                var venuesAsDto = _mapper.Map<IEnumerable<VenueAdminResponse>>(venues);

                return ServiceResult<IEnumerable<VenueAdminResponse>>.Success(venuesAsDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<VenueAdminResponse>>.Fail($"Bir hata oluştu: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResult<VenueResponse>> GetByIdAsync(int id)
        {
            try
            {
                await _venueRules.CheckIfVenueExists(id);

                var venue = await _unitOfWork.Venues.GetByIdAsync(id);

                var venueAsDto = _mapper.Map<VenueResponse>(venue);

                return ServiceResult<VenueResponse>.Success(venueAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<VenueResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<VenueResponse>>> GetPagedAllVenuesAsync(int pageNumber, int pageSize)
        {
            try
            {
                _venueRules.CheckIfPagingParametersAreValid(pageNumber, pageSize);

                var venues = await _unitOfWork.Venues.GetAllPagedAsync(pageNumber, pageSize);
                var venuesAsDto = _mapper.Map<IEnumerable<VenueResponse>>(venues);

                return ServiceResult<IEnumerable<VenueResponse>>.Success(venuesAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<IEnumerable<VenueResponse>>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<VenueAggregateResponse>> GetVenueAggregateAsync(int id)
        {
            try
            {
                await _venueRules.CheckIfVenueExists(id);

                var venue = await _unitOfWork.Venues.GetVenueAggregateAsync(id);

                var venueAsDto = _mapper.Map<VenueAggregateResponse>(venue);

                return ServiceResult<VenueAggregateResponse>.Success(venueAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<VenueAggregateResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<VenueAdminResponse>> GetVenueForAdminAsync(int id)
        {
            try
            {
                await _venueRules.CheckIfVenueExists(id);

                var venue = await _unitOfWork.Venues.GetVenueForAdminAsync(id);

                var venueAsDto = _mapper.Map<VenueAdminResponse>(venue);

                return ServiceResult<VenueAdminResponse>.Success(venueAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<VenueAdminResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<VenueAggregateResponse>>> GetVenuesAggregateAsync()
        {
            try
            {
                var venues = await _unitOfWork.Venues.GetVenuesAggregateAsync();
                var venuesAsDto = _mapper.Map<IEnumerable<VenueAggregateResponse>>(venues);

                return ServiceResult<IEnumerable<VenueAggregateResponse>>.Success(venuesAsDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<VenueAggregateResponse>>.Fail($"Bir hata oluştu: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResult<IEnumerable<VenueWithEventsResponse>>> GetVenuesWithEventsAsync()
        {
            try
            {
                var venues = await _unitOfWork.Venues.GetVenuesWithEventsAsync();
                var venuesAsDto = _mapper.Map<IEnumerable<VenueWithEventsResponse>>(venues);

                return ServiceResult<IEnumerable<VenueWithEventsResponse>>.Success(venuesAsDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<VenueWithEventsResponse>>.Fail($"Bir hata oluştu: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResult<VenueWithEventsResponse>> GetVenueWithEventsAsync(int id)
        {
            try
            {
                await _venueRules.CheckIfVenueExists(id);

                var venue = await _unitOfWork.Venues.GetVenueWithEventsAsync(id);
                var venueAsDto = _mapper.Map<VenueWithEventsResponse>(venue);

                return ServiceResult<VenueWithEventsResponse>.Success(venueAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<VenueWithEventsResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult> PassiveAsync(int id)
        {
            try
            {
                await _venueRules.CheckIfVenueExists(id);
                await _venueRules.CheckIfVenueHasActiveEvents(id);

                await _unitOfWork.Venues.PassiveAsync(id);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.Success(HttpStatusCode.NoContent);
            }
            catch (BusinessException ex)
            {
                return ServiceResult.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult> UpdateAsync(UpdateVenueRequest request)
        {
            try
            {
                await _venueRules.CheckIfVenueNameExistsWhenUpdating(request.Id, request.Name);
                await _venueRules.CheckIfDistrictExists(request.DistrictId);
                await _venueRules.CheckIfVenueExists(request.Id);

                var venue = await _unitOfWork.Venues.GetByIdAsync(request.Id);

                _mapper.Map(request, venue);

                _unitOfWork.Venues.Update(venue!);
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
