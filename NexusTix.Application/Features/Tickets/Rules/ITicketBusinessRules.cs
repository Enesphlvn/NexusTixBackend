namespace NexusTix.Application.Features.Tickets.Rules
{
    public interface ITicketBusinessRules
    {
        Task CheckIfEventExists(int eventId);
        Task CheckIfUserExists(int userId);
        Task CheckIfEventHasCapacity(int eventId);
        Task CheckIfTicketExistsByQrCode(Guid qrCode);
        Task CheckIfTicketIsAlreadyUsed(Guid qrCode);
    }
}
