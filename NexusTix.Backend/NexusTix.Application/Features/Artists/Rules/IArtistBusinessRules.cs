using NexusTix.Application.Common.BaseRules;

namespace NexusTix.Application.Features.Artists.Rules
{
    public interface IArtistBusinessRules : IBaseBusinessRules
    {
        Task CheckIfArtistExists(int id);
        Task CheckIfArtistNameExistsWhenCreating(string artistName);
        Task CheckIfArtistNameExistsWhenUpdating(int artistId, string artistName);
        Task CheckIfArtistHasActiveFutureEvents(int artistId);
    }
}
