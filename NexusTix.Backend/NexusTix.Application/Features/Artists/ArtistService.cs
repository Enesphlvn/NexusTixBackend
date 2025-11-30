using AutoMapper;
using NexusTix.Application.Features.Artists.Create;
using NexusTix.Application.Features.Artists.Responses;
using NexusTix.Application.Features.Artists.Rules;
using NexusTix.Application.Features.Artists.Update;
using NexusTix.Application.Features.Venues.Responses;
using NexusTix.Domain.Entities;
using NexusTix.Domain.Exceptions;
using NexusTix.Persistence.Repositories;
using System.Net;

namespace NexusTix.Application.Features.Artists
{
    public class ArtistService : IArtistService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IArtistBusinessRules _artistRules;

        public ArtistService(IUnitOfWork unitOfWork, IMapper mapper, IArtistBusinessRules artistRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _artistRules = artistRules;
        }

        public async Task<ServiceResult<ArtistResponse>> CreateAsync(CreateArtistRequest request)
        {
            try
            {
                await _artistRules.CheckIfArtistNameExistsWhenCreating(request.Name);

                var artist = _mapper.Map<Artist>(request);

                await _unitOfWork.Artists.AddAsync(artist);
                await _unitOfWork.SaveChangesAsync();

                var response = _mapper.Map<ArtistResponse>(artist);

                return ServiceResult<ArtistResponse>.SuccessAsCreated(response, $"api/artists/{artist.Id}");
            }
            catch (BusinessException ex)
            {
                return ServiceResult<ArtistResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            try
            {
                await _artistRules.CheckIfArtistExists(id);
                await _artistRules.CheckIfArtistHasActiveFutureEvents(id);

                var artist = await _unitOfWork.Artists.GetByIdAsync(id);

                _unitOfWork.Artists.Delete(artist!);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.Success(HttpStatusCode.NoContent);
            }
            catch (BusinessException ex)
            {
                return ServiceResult.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<ArtistResponse>>> GetAllArtistsAsync()
        {
            try
            {
                var artists = await _unitOfWork.Artists.GetAllAsync();

                var artistsAsDto = _mapper.Map<IEnumerable<ArtistResponse>>(artists);

                return ServiceResult<IEnumerable<ArtistResponse>>.Success(artistsAsDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<ArtistResponse>>.Fail($"Bir hata oluştu: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResult<IEnumerable<ArtistAdminResponse>>> GetAllArtistsForAdminAsync()
        {
            try
            {
                var artists = await _unitOfWork.Artists.GetAllArtistsForAdminAsync();

                var artistsAsDto = _mapper.Map<IEnumerable<ArtistAdminResponse>>(artists);

                return ServiceResult<IEnumerable<ArtistAdminResponse>>.Success(artistsAsDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<ArtistAdminResponse>>.Fail($"Bir hata oluştu: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResult<ArtistAdminResponse>> GetArtistForAdminAsync(int id)
        {
            try
            {
                var artist = await _unitOfWork.Artists.GetArtistForAdminAsync(id);

                if (artist == null)
                {
                    return ServiceResult<ArtistAdminResponse>.Fail("Sanatçı bulunamadı.", HttpStatusCode.NotFound);
                }

                var artistAsDto = _mapper.Map<ArtistAdminResponse>(artist);

                return ServiceResult<ArtistAdminResponse>.Success(artistAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<ArtistAdminResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<ArtistResponse>>> GetArtistsByEventTypeAsync(int eventTypeId)
        {
            try
            {
                var artists = await _unitOfWork.Artists.GetArtistsByEventTypeAsync(eventTypeId);
                var artistsAsDto = _mapper.Map<IEnumerable<ArtistResponse>>(artists);

                return ServiceResult<IEnumerable<ArtistResponse>>.Success(artistsAsDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<ArtistResponse>>.Fail(ex.Message);
            }
        }

        public async Task<ServiceResult<IEnumerable<ArtistWithEventsResponse>>> GetArtistsWithEventsAsync()
        {
            try
            {
                var artists = await _unitOfWork.Artists.GetArtistsWithEventsAsync();

                var artistsAsDto = _mapper.Map<IEnumerable<ArtistWithEventsResponse>>(artists);

                return ServiceResult<IEnumerable<ArtistWithEventsResponse>>.Success(artistsAsDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<ArtistWithEventsResponse>>.Fail($"Bir hata oluştu: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResult<ArtistWithEventsResponse>> GetArtistWithEventsAsync(int id)
        {
            try
            {
                await _artistRules.CheckIfArtistExists(id);

                var artist = await _unitOfWork.Artists.GetArtistWithEventsAsync(id);

                var artistAsDto = _mapper.Map<ArtistWithEventsResponse>(artist);

                return ServiceResult<ArtistWithEventsResponse>.Success(artistAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<ArtistWithEventsResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<ArtistResponse>> GetByIdAsync(int id)
        {
            try
            {
                await _artistRules.CheckIfArtistExists(id);

                var artist = await _unitOfWork.Artists.GetByIdAsync(id);

                var artistAsDto = _mapper.Map<ArtistResponse>(artist);

                return ServiceResult<ArtistResponse>.Success(artistAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<ArtistResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<ArtistResponse>>> GetPagedAllArtistsAsync(int pageNumber, int pageSize)
        {
            try
            {
                _artistRules.CheckIfPagingParametersAreValid(pageNumber, pageSize);

                var artists = await _unitOfWork.Artists.GetAllPagedAsync(pageNumber, pageSize);

                var artistsAsDto = _mapper.Map<IEnumerable<ArtistResponse>>(artists);

                return ServiceResult<IEnumerable<ArtistResponse>>.Success(artistsAsDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<ArtistResponse>>.Fail($"Bir hata oluştu: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResult> PassiveAsync(int id)
        {
            try
            {
                var newArtist = await _unitOfWork.Artists.GetByIdIncludingPassiveAsync(id);

                if (newArtist == null)
                {
                    return ServiceResult.Fail("Sanatçı bulunamadı.", HttpStatusCode.NotFound);
                }

                if (newArtist.IsActive)
                {
                    await _artistRules.CheckIfArtistHasActiveFutureEvents(id);
                }

                newArtist.IsActive = !newArtist.IsActive;

                _unitOfWork.Artists.Update(newArtist);

                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.Success(HttpStatusCode.NoContent);
            }
            catch (BusinessException ex)
            {
                return ServiceResult.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult> UpdateAsync(UpdateArtistRequest request)
        {
            try
            {
                await _artistRules.CheckIfArtistNameExistsWhenUpdating(request.Id, request.Name);

                var artist = await _unitOfWork.Artists.GetByIdIncludingPassiveAsync(request.Id);

                if (artist == null)
                {
                    return ServiceResult.Fail("Sanatçı bulunamadı.", HttpStatusCode.NotFound);
                }

                _mapper.Map(request, artist);

                _unitOfWork.Artists.Update(artist!);
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
