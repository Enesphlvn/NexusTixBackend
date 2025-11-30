using NexusTix.Application.Common.BaseRules;

namespace NexusTix.Application.Features.Artists.Rules
{
    public interface IArtistBusinessRules : IBaseBusinessRules
    {
        Task CheckIfArtistExists(int artistId);
        Task CheckIfEventTypeExists(int eventTypeId);
        Task CheckIfArtistNameExistsWhenCreating(string artistName);
        Task CheckIfArtistNameExistsWhenUpdating(int artistId, string artistName);
        Task CheckIfArtistHasActiveFutureEvents(int artistId);
    }
}
