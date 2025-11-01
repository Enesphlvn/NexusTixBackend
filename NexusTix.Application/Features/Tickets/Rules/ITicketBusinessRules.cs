using NexusTix.Application.Common.BaseRules;

namespace NexusTix.Application.Features.Tickets.Rules
{
    public interface ITicketBusinessRules : IBaseBusinessRules
    {
        Task CheckIfEventExists(int eventId);
        Task CheckIfUserExists(int userId);
        Task CheckIfEventHasCapacity(int eventId);
        Task CheckIfTicketExistsByQrCode(Guid qrCode);
        Task CheckIfTicketIsAlreadyUsed(Guid qrCode);
    }
}
