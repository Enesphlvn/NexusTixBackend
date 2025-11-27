using NexusTix.Application.Features.Artists.Create;
using NexusTix.Application.Features.Artists.Responses;
using NexusTix.Application.Features.Artists.Update;

namespace NexusTix.Application.Features.Artists
{
    public class ArtistService : IArtistService
    {
        public Task<ServiceResult<ArtistResponse>> CreateAsync(CreateArtistRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<IEnumerable<ArtistResponse>>> GetAllArtistsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<IEnumerable<ArtistWithEventsResponse>>> GetArtistsWithEventsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<ArtistWithEventsResponse>> GetArtistWithEventsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<ArtistResponse>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<IEnumerable<ArtistResponse>>> GetPagedAllArtistsAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> PassiveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> UpdateAsync(UpdateArtistRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
