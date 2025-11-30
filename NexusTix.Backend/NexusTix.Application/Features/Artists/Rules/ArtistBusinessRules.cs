using NexusTix.Application.Common.BaseRules;
using NexusTix.Domain.Exceptions;
using NexusTix.Persistence.Repositories;
using System.Net;

namespace NexusTix.Application.Features.Artists.Rules
{
    public class ArtistBusinessRules : BaseBusinessRules, IArtistBusinessRules
    {
        private readonly IUnitOfWork _unitOfWork;

        public ArtistBusinessRules(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task CheckIfArtistExists(int artistId)
        {
            var exists = await _unitOfWork.Artists.AnyAsync(artistId);
            if (!exists)
            {
                throw new BusinessException($"ID'si '{artistId}' olan sanatçı bulunamadı.", HttpStatusCode.NotFound);
            }
        }

        public async Task CheckIfArtistHasActiveFutureEvents(int artistId)
        {
            var hasFutureEvents = await _unitOfWork.Events.AnyAsync
                (
                    x => x.Artists.Any(x => x.Id == artistId) && x.Date > DateTimeOffset.UtcNow
                );

            if (hasFutureEvents)
            {
                throw new BusinessException($"ID'si '{artistId}' olan sanatçının gelecekte planlanmış aktif etkinlikleri mevcut. İşlem gerçekleştirilemez.", HttpStatusCode.Conflict);
            }
        }

        public async Task CheckIfArtistNameExistsWhenCreating(string artistName)
        {
            var exists = await _unitOfWork.Artists.AnyAsync(x => x.Name.ToLower() == artistName.ToLower());
            if (exists)
            {
                throw new BusinessException($"Sanatçı adı: '{artistName}'. Bu isimde başka bir sanatçı mevcut", HttpStatusCode.Conflict);
            }
        }

        public async Task CheckIfArtistNameExistsWhenUpdating(int artistId, string artistName)
        {
            var exists = await _unitOfWork.Artists.AnyAsync(x => x.Name.ToLower() == artistName.ToLower() && x.Id != artistId);
            if (exists)
            {
                throw new BusinessException($"Sanatçı adı: '{artistId}'. Bu isimde başka bir sanatçı mevcut.", HttpStatusCode.Conflict);
            }
        }

        public async Task CheckIfEventTypeExists(int eventTypeId)
        {
            var exists = await _unitOfWork.EventTypes.AnyAsync(eventTypeId);
            if (!exists)
            {
                throw new BusinessException($"ID'si '{eventTypeId}' olan etkinlik türü bulunamadı.", HttpStatusCode.NotFound);
            }
        }
    }
}
