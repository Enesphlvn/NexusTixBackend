using NexusTix.Application.Features.Artists.Create;
using NexusTix.Application.Features.Artists.Responses;
using NexusTix.Application.Features.Artists.Update;
using NexusTix.Domain.Entities;

namespace NexusTix.Application.Features.Artists
{
    public interface IArtistService
    {
        Task<ServiceResult<IEnumerable<ArtistResponse>>> GetAllArtistsAsync();
        Task<ServiceResult<IEnumerable<ArtistResponse>>> GetPagedAllArtistsAsync(int pageNumber, int pageSize);
        Task<ServiceResult<ArtistResponse>> GetByIdAsync(int id);

        Task<ServiceResult<ArtistWithEventsResponse>> GetArtistWithEventsAsync(int id);
        Task<ServiceResult<IEnumerable<ArtistWithEventsResponse>>> GetArtistsWithEventsAsync();
        Task<ServiceResult<IEnumerable<ArtistAdminResponse>>> GetAllArtistsForAdminAsync();
        Task<ServiceResult<ArtistAdminResponse>> GetArtistForAdminAsync(int id);
        Task<ServiceResult<IEnumerable<ArtistResponse>>> GetArtistsByEventTypeAsync(int eventTypeId);

        Task<ServiceResult<ArtistResponse>> CreateAsync(CreateArtistRequest request);
        Task<ServiceResult> UpdateAsync(UpdateArtistRequest request);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult> PassiveAsync(int id);
    }
}
