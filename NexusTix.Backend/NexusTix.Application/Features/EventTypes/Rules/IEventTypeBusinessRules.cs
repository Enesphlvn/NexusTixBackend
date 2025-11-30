using NexusTix.Application.Common.BaseRules;

namespace NexusTix.Application.Features.EventTypes.Rules
{
    public interface IEventTypeBusinessRules : IBaseBusinessRules
    {
        Task CheckIfEventTypeExists(int eventTypeId);
        Task CheckIfArtistExists(int artistId);
        Task CheckIfEventTypeNameExistsWhenCreating(string eventTypeName);
        Task CheckIfEventTypeNameExistsWhenUpdating(int eventTypeId, string eventTypeName);
        Task CheckIfEventTypeHasNoEvents(int eventTypeId);
    }
}
