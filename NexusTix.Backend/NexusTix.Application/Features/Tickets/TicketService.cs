using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NexusTix.Application.Features.Tickets.CheckIn;
using NexusTix.Application.Features.Tickets.Create;
using NexusTix.Application.Features.Tickets.Responses;
using NexusTix.Application.Features.Tickets.Rules;
using NexusTix.Domain.Entities;
using NexusTix.Domain.Exceptions;
using NexusTix.Persistence.Repositories;
using System.Net;

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

        public async Task<ServiceResult> CancelTicketAsync(int ticketId, int userId)
        {
            try
            {
                await _ticketRules.CheckIfTicketExists(ticketId);

                var ticket = await _unitOfWork.Tickets.GetByIdAsync(ticketId);

                await _ticketRules.CheckIfTicketCanBeCancelled(ticketId);

                ticket!.IsCancelled = true;

                _unitOfWork.Tickets.Update(ticket);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.Success(HttpStatusCode.NoContent);
            }
            catch (BusinessException ex)
            {
                return ServiceResult.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult> CheckInAsync(CheckInTicketRequest request)
        {
            try
            {
                await _ticketRules.CheckIfTicketExistsByQrCode(request.QRCodeGuid);
                await _ticketRules.CheckIfTicketBelongsToEvent(request.QRCodeGuid, request.EventId);
                await _ticketRules.CheckIfTicketIsAlreadyUsed(request.QRCodeGuid);

                var ticket = await _unitOfWork.Tickets.WhereTracked(x => x.QRCodeGuid == request.QRCodeGuid).FirstOrDefaultAsync();

                ticket!.IsUsed = true;
                _unitOfWork.Tickets.Update(ticket);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.Success(HttpStatusCode.OK);
            }
            catch (BusinessException ex)
            {
                return ServiceResult.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<TicketResponse>> CreateAsync(CreateTicketRequest request, int userId)
        {
            try
            {
                await _ticketRules.CheckIfUserExists(userId);
                await _ticketRules.CheckIfEventExists(request.EventId);
                await _ticketRules.CheckIfEventIsPast(request.EventId);
                await _ticketRules.CheckIfEventHasCapacity(request.EventId);

                var newTicket = _mapper.Map<Ticket>(request);

                newTicket.UserId = userId;
                newTicket.QRCodeGuid = Guid.NewGuid();
                newTicket.PurchaseDate = DateTimeOffset.UtcNow;
                newTicket.IsUsed = false;

                await _unitOfWork.Tickets.AddAsync(newTicket);
                await _unitOfWork.SaveChangesAsync();

                var ticketAsDto = _mapper.Map<TicketResponse>(newTicket);

                return ServiceResult<TicketResponse>.SuccessAsCreated(ticketAsDto, $"api/tickets/{newTicket.Id}");
            }
            catch (BusinessException ex)
            {
                return ServiceResult<TicketResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            try
            {
                await _ticketRules.CheckIfTicketExists(id);

                var ticket = await _unitOfWork.Tickets.GetByIdAsync(id);

                _unitOfWork.Tickets.Delete(ticket!);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.Success(HttpStatusCode.NoContent);
            }
            catch (BusinessException ex)
            {
                return ServiceResult.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<TicketResponse>>> GetAllTicketsAsync()
        {
            try
            {
                var tickets = await _unitOfWork.Tickets.GetAllAsync();
                var ticketsAsDto = _mapper.Map<IEnumerable<TicketResponse>>(tickets);

                return ServiceResult<IEnumerable<TicketResponse>>.Success(ticketsAsDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<TicketResponse>>.Fail($"Bir hata oluştu: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResult<TicketResponse>> GetByIdAsync(int id)
        {
            try
            {
                await _ticketRules.CheckIfTicketExists(id);

                var ticket = await _unitOfWork.Tickets.GetByIdAsync(id);

                var ticketAsDto = _mapper.Map<TicketResponse>(ticket);

                return ServiceResult<TicketResponse>.Success(ticketAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<TicketResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<TicketResponse>>> GetPagedAllTicketsAsync(int pageNumber, int pageSize)
        {
            try
            {
                _ticketRules.CheckIfPagingParametersAreValid(pageNumber, pageSize);

                var tickets = await _unitOfWork.Tickets.GetAllPagedAsync(pageNumber, pageSize);
                var ticketsAsDto = _mapper.Map<IEnumerable<TicketResponse>>(tickets);

                return ServiceResult<IEnumerable<TicketResponse>>.Success(ticketsAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<IEnumerable<TicketResponse>>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<TicketAggregateResponse>> GetTicketAggregateAsync(int id)
        {
            try
            {
                await _ticketRules.CheckIfTicketExists(id);

                var ticket = await _unitOfWork.Tickets.GetTicketAggregateAsync(id);

                var ticketAsDto = _mapper.Map<TicketAggregateResponse>(ticket);

                return ServiceResult<TicketAggregateResponse>.Success(ticketAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<TicketAggregateResponse>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<int>> GetTicketCountByEventAsync(int eventId)
        {
            try
            {
                await _ticketRules.CheckIfEventExists(eventId);

                var count = await _unitOfWork.Tickets.GetTicketCountByEventAsync(eventId);

                return ServiceResult<int>.Success(count);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<int>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<TicketAggregateResponse>>> GetTicketsAggregateAsync()
        {
            try
            {
                var tickets = await _unitOfWork.Tickets.GetTicketsAggregateAsync();

                var ticketsAsDto = _mapper.Map<IEnumerable<TicketAggregateResponse>>(tickets);

                return ServiceResult<IEnumerable<TicketAggregateResponse>>.Success(ticketsAsDto);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<TicketAggregateResponse>>.Fail($"Bir hata oluştu: {ex.Message}", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ServiceResult<IEnumerable<TicketByDateRangeResponse>>> GetTicketsByDateRangeAsync(DateTimeOffset startDate, DateTimeOffset endDate)
        {
            try
            {
                _ticketRules.CheckIfDateRangeIsValid(startDate, endDate);

                var tickets = await _unitOfWork.Tickets.GetTicketsByDateRangeAsync(startDate, endDate);
                var ticketsAsDto = _mapper.Map<IEnumerable<TicketByDateRangeResponse>>(tickets);

                return ServiceResult<IEnumerable<TicketByDateRangeResponse>>.Success(ticketsAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<IEnumerable<TicketByDateRangeResponse>>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<TicketByEventResponse>>> GetTicketsByEventAsync(int eventId)
        {
            try
            {
                await _ticketRules.CheckIfEventExists(eventId);

                var tickets = await _unitOfWork.Tickets.GetTicketsByEventAsync(eventId);
                var ticketsAsDto = _mapper.Map<IEnumerable<TicketByEventResponse>>(tickets);

                return ServiceResult<IEnumerable<TicketByEventResponse>>.Success(ticketsAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<IEnumerable<TicketByEventResponse>>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<IEnumerable<TicketByUserResponse>>> GetTicketsByUserAsync(int userId)
        {
            try
            {
                await _ticketRules.CheckIfUserExists(userId);

                var tickets = await _unitOfWork.Tickets.GetTicketsByUserAsync(userId);
                var ticketsAsDto = _mapper.Map<IEnumerable<TicketByUserResponse>>(tickets);

                return ServiceResult<IEnumerable<TicketByUserResponse>>.Success(ticketsAsDto);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<IEnumerable<TicketByUserResponse>>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult<bool>> HasUserTicketForEventAsync(int userId, int eventId)
        {
            try
            {
                await _ticketRules.CheckIfUserExists(userId);
                await _ticketRules.CheckIfEventExists(eventId);

                bool hasTicket = await _unitOfWork.Tickets.HasUserTicketForEventAsync(userId, eventId);

                return ServiceResult<bool>.Success(hasTicket);
            }
            catch (BusinessException ex)
            {
                return ServiceResult<bool>.Fail(ex.Message, ex.StatusCode);
            }
        }

        public async Task<ServiceResult> PassiveAsync(int id)
        {
            try
            {
                await _ticketRules.CheckIfTicketExists(id);

                await _unitOfWork.Tickets.PassiveAsync(id);
                await _unitOfWork.SaveChangesAsync();

                return ServiceResult.Success(HttpStatusCode.NoContent);
            }
            catch (BusinessException ex)
            {
                return ServiceResult.Fail(ex.Message, ex.StatusCode);
            }
        }
    }
}