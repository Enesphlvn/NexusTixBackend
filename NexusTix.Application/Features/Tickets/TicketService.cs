using AutoMapper;
using NexusTix.Application.Features.Tickets.CheckIn;
using NexusTix.Application.Features.Tickets.Create;
using NexusTix.Application.Features.Tickets.Responses;
using NexusTix.Application.Features.Tickets.Rules;
using NexusTix.Persistence.Repositories;

namespace NexusTix.Application.Features.Tickets
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITicketBusinessRules _ticketRules;

        public TicketService(IUnitOfWork unitOfWork, IMapper mapper, ITicketBusinessRules ticketRules)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _ticketRules = ticketRules;
        }

        public Task<ServiceResult> CheckInAsync(CheckInTicketRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<TicketResponse>> CreateAsync(CreateTicketRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<IEnumerable<TicketResponse>>> GetAllTicketsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<TicketResponse>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<IEnumerable<TicketResponse>>> GetPagedAllTicketsAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<TicketAggregateResponse>> GetTicketAggregateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<int>> GetTicketCountByEventAsync(int eventId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<IEnumerable<TicketAggregateResponse>>> GetTicketsAggregateAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<IEnumerable<TicketByDateRangeResponse>>> GetTicketsByDateRangeAsync(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<IEnumerable<TicketByEventResponse>>> GetTicketsByEventAsync(int eventId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<IEnumerable<TicketByUserResponse>>> GetTicketsByUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult<bool>> HasUserTicketForEventAsync(int userId, int eventId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResult> PassiveAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}