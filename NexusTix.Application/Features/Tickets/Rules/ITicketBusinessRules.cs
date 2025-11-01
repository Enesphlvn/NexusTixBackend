using NexusTix.Application.Common.BaseRules;

namespace NexusTix.Application.Features.Tickets.Rules
{
    public interface ITicketBusinessRules : IBaseBusinessRules
    {
        Task CheckIfEventExists(int eventId);
        Task CheckIfUserExists(int userId);
        Task CheckIfTicketExists(int ticketId);
        Task CheckIfEventHasCapacity(int eventId);
        Task CheckIfTicketExistsByQrCode(Guid qrCode);
        Task CheckIfTicketIsAlreadyUsed(Guid qrCode);
        void CheckIfDateRangeIsValid(DateTimeOffset startDate, DateTimeOffset endDate);
    }
}
