using NexusTix.Application.Features.Tickets.CheckIn;
using NexusTix.Application.Features.Tickets.Create;
using NexusTix.Application.Features.Tickets.Responses;

namespace NexusTix.Application.Features.Tickets
{
    public interface ITicketService
    {
        Task<ServiceResult<IEnumerable<TicketResponse>>> GetAllTicketsAsync();
        Task<ServiceResult<IEnumerable<TicketResponse>>> GetPagedAllTicketsAsync(int pageNumber, int pageSize);
        Task<ServiceResult<TicketResponse>> GetByIdAsync(int id);

        Task<ServiceResult<TicketAggregateResponse>> GetTicketAggregateAsync(int id);
        Task<ServiceResult<IEnumerable<TicketAggregateResponse>>> GetTicketsAggregateAsync();
        Task<ServiceResult<IEnumerable<TicketByEventResponse>>> GetTicketsByEventAsync(int eventId);
        Task<ServiceResult<IEnumerable<TicketByUserResponse>>> GetTicketsByUserAsync(int userId);
        Task<ServiceResult<IEnumerable<TicketByDateRangeResponse>>> GetTicketsByDateRangeAsync(DateTimeOffset startDate, DateTimeOffset endDate);
        Task<ServiceResult<bool>> HasUserTicketForEventAsync(int userId, int eventId);
        Task<ServiceResult<int>> GetTicketCountByEventAsync(int eventId);

        Task<ServiceResult<TicketResponse>> CreateAsync(CreateTicketRequest request);
        Task<ServiceResult> CheckInAsync(CheckInTicketRequest request);
        Task<ServiceResult> DeleteAsync(int id);
        Task<ServiceResult> PassiveAsync(int id);
    }
}
